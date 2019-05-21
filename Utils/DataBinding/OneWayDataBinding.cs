using System;
using System.ComponentModel;
using System.Reflection;
using Hermit.DataBinding;
using UnityEngine;
using Component = UnityEngine.Component;

namespace Hermit
{
    public class OneWayDataBinding : DataBindingBase
    {
        public Component DataProvider;

        [SerializeField]
        private string _viewModelPropertyName;

        [SerializeField]
        private string _viewPropertyName;

        [SerializeField]
        private string _adapterTypeName;

        [SerializeField]
        private AdapterOptions _adapterOptions;

        #region Properties

        public string ViewModelPropertyName
        {
            get => _viewModelPropertyName;
            set => _viewModelPropertyName = value;
        }

        public string ViewPropertyName
        {
            get => _viewPropertyName;
            set => _viewPropertyName = value;
        }

        public string AdapterTypeName
        {
            get => _adapterTypeName;
            set => _adapterTypeName = value;
        }

        public AdapterOptions AdapterOptions
        {
            get => _adapterOptions;
            set => _adapterOptions = value;
        }

        #endregion

        #region Runtime Variables

        private ViewModel _viewModel;

        private string _viewModelMemberName;

        private IAdapter _adapterInstance;

        private Action<object> _viewSetter;

        private Func<object> _viewModelGetter;

        #endregion

        protected void Awake()
        {
            if (DataProvider is IViewModelProvider provider) { _viewModel = provider.GetViewModel() as ViewModel; }

            if (string.IsNullOrEmpty(_adapterTypeName)) { return; }

            _adapterInstance = Her.Resolve<IAdapter>(_adapterTypeName);
        }

        private void OnEnable()
        {
            if (_viewModel != null) { _viewModel.PropertyChanged += OnPropertyChanged; }

            Bind(ViewPropertyName, ViewModelPropertyName);
        }

        private void OnDisable()
        {
            if (_viewModel != null) { _viewModel.PropertyChanged -= OnPropertyChanged; }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_viewModelMemberName != e.PropertyName) { return; }

            var rawValue = _viewModelGetter.Invoke();
            var convertedValue = _adapterInstance?.Covert(rawValue, _adapterOptions);
            _viewSetter.Invoke(_adapterInstance != null ? convertedValue : rawValue);
        }

        private void Bind(string viewPropertyName, string viewModelPropertyName)
        {
            #region View 

            var (typeName, memberName) = ParseEndPointReference(viewPropertyName);

            var component = GetComponent(typeName);
            if (component == null) { throw new Exception($"Can't find component of type: {typeName}."); }

            var viewMemberInfos = component.GetType().GetMember(memberName);
            if (viewMemberInfos.Length <= 0)
            {
                throw new Exception($"Can't find member of name: {memberName} on {component}.");
            }

            var memberInfo = viewMemberInfos[0];

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    var fieldInfo = memberInfo as FieldInfo;
                    _viewSetter = value => fieldInfo?.SetValue(component, value);
                    break;
                case MemberTypes.Property:
                    var propertyInfo = memberInfo as PropertyInfo;
                    _viewSetter = value => propertyInfo?.SetValue(component, value);
                    break;
                default:
                    throw new Exception($"MemberType: {memberName} is not supported in one way property binding.");
            }

            #endregion

            #region View Model

            (_, _viewModelMemberName) = ParseEndPointReference(viewModelPropertyName);

            var viewModelMemberInfos = _viewModel.GetType().GetMember(_viewModelMemberName);
            if (viewModelMemberInfos.Length <= 0)
            {
                throw new Exception($"Can't find member of name: {_viewModelMemberName} on {_viewModel}.");
            }

            memberInfo = viewModelMemberInfos[0];

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    var fieldInfo = memberInfo as FieldInfo;
                    _viewModelGetter = () => fieldInfo?.GetValue(_viewModel);
                    break;
                case MemberTypes.Property:
                    var propertyInfo = memberInfo as PropertyInfo;
                    _viewModelGetter = () => propertyInfo?.GetValue(_viewModel);
                    break;
                default:
                    throw new Exception($"MemberType: {memberName} is not supported in one way property binding.");
            }

            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using VSSystem.ThirdParty.Selenium.Define;

namespace VSSystem.ThirdParty.Selenium.Actions
{
    [Newtonsoft.Json.JsonObject(ItemNullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public class ElementProps
    {
        string _ID;
        public string ID { get { return _ID; } set { _ID = value; } }
        string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        string _XPath;
        public string XPath { get { return _XPath; } set { _XPath = value; } }
        ClassProps _ClassItem;
        public ClassProps ClassItem { get { return _ClassItem; } set { _ClassItem = value; } }
        string _Type;
        public string Type { get { return _Type; } set { _Type = value; } }
        [Newtonsoft.Json.JsonIgnore]
        public EElementType EType
        {
            get
            {
                EElementType result = EElementType.Undefine;
                if (!string.IsNullOrWhiteSpace(_Type))
                {
                    Enum.TryParse(_Type, true, out result);
                }
                return result;
            }
        }

        string _IFrameID;
        public string IFrameID { get { return _IFrameID; } set { _IFrameID = value; } }
        string _ParentID;
        public string ParentID { get { return _ParentID; } set { _ParentID = value; } }
        IWebElement _ParentElement;
        [Newtonsoft.Json.JsonIgnore]
        public IWebElement ParentElement { get { return _ParentElement; } set { _ParentElement = value; } }
        TagProps _TagItem;
        public TagProps TagItem { get { return _TagItem; } set { _TagItem = value; } }
        bool? _SwitchToNewWindow;
        public bool? SwitchToNewWindow { get { return _SwitchToNewWindow; } set { _SwitchToNewWindow = value; } }
        bool? _CloseWindow;
        public bool? CloseWindow { get { return _CloseWindow; } set { _CloseWindow = value; } }
        string _Value;
        public string Value { get { return _Value; } set { _Value = value; } }
        string _Text;
        public string Text { get { return _Text; } set { _Text = value; } }
        bool? _Checked;
        public bool? Checked { get { return _Checked; } set { _Checked = value; } }
        bool? _Displayed;
        public bool? Displayed { get { return _Displayed; } set { _Displayed = value; } }

        List<WebAction> _Actions;
        public List<WebAction> Actions { get { return _Actions; } set { _Actions = value; } }
        public IWebElement GetWebElement(IWebDriver driver, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            return _GetWebElement(driver, debugLogAction, errorLogAction);
        }
        IWebElement _GetWebElement(ISearchContext searchCtx, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            IWebElement elementObj = null;
            if (_ParentElement != null)
            {
                searchCtx = _ParentElement;
            }
            else if (!string.IsNullOrWhiteSpace(_ParentID))
            {
#if DEBUG
                if (_ParentID == "3dOptionCtr")
                {

                }

#endif
                try
                {
                    searchCtx = searchCtx.FindElement(By.Id(_ParentID));
                }
                catch (Exception ex)
                {
                    errorLogAction?.Invoke(new Exception("FindElement(By.ParentID(" + _ParentID, ex));
                }
            }

            if (!string.IsNullOrWhiteSpace(_ID))
            {
#if DEBUG
                if (_ID == "dvNotification")
                {

                }
#endif
                try
                {
                    elementObj = searchCtx.FindElement(By.Id(_ID));
                }
                catch (Exception ex)
                {
                    errorLogAction?.Invoke(new Exception("FindElement(By.Id(" + _ID, ex));
                }
            }
            if (elementObj == null)
            {
                if (!string.IsNullOrWhiteSpace(_Name))
                {
                    try
                    {
                        elementObj = searchCtx.FindElement(By.Name(_Name));
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("FindElement(By.Name(" + _Name, ex));
                    }
                }
            }
            if (elementObj == null)
            {
                if (!string.IsNullOrWhiteSpace(_XPath))
                {
                    try
                    {

                        elementObj = searchCtx.FindElement(By.XPath(_XPath));
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("FindElement(By.XPath(" + _XPath, ex));
                    }
                }
            }
            if (elementObj == null)
            {

                if (!string.IsNullOrWhiteSpace(_ClassItem?.ClassName))
                {
                    try
                    {
                        var foundElementObjs = searchCtx.FindElements(By.ClassName(_ClassItem.ClassName))?.Where(ite => ite.Displayed)?.ToList();
                        if (foundElementObjs?.Count > 0)
                        {
                            if (_ClassItem.Attributes?.Count > 0)
                            {
                                foreach (var attrObj in _ClassItem.Attributes)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => attrObj.ValidPredicate(ite.GetAttribute(attrObj.Name)))
                                    ?.ToList();
                                }
                            }
                            if (_ClassItem.Values?.Count > 0)
                            {
                                var values = _ClassItem.Values.Where(ite => !string.IsNullOrWhiteSpace(ite)).ToList();
                                if (values?.Count > 0)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => values.Contains(ite.GetAttribute("value")?.Trim(), StringComparer.InvariantCultureIgnoreCase))
                                    ?.ToList();
                                }

                            }
                            if (!string.IsNullOrWhiteSpace(_ClassItem.Value))
                            {
                                foundElementObjs = foundElementObjs
                                ?.Where(ite => ite.GetAttribute("value")?.Trim()?.Equals(_ClassItem.Value, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ?.ToList();
                            }
                            if (_ClassItem.Texts?.Count > 0)
                            {
                                var texts = _ClassItem.Texts.Where(ite => !string.IsNullOrWhiteSpace(ite)).ToList();
                                if (texts?.Count > 0)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => texts.Contains(ite.Text, StringComparer.InvariantCultureIgnoreCase))
                                    ?.ToList();
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(_ClassItem.Text))
                            {
                                foundElementObjs = foundElementObjs
                                ?.Where(ite => ite.Text?.Trim()?.Equals(_ClassItem.Text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ?.ToList();
                            }

                            if (foundElementObjs?.Count > 0)
                            {
                                int index = _ClassItem.Index ?? foundElementObjs.Count;
                                if (index < foundElementObjs.Count)
                                {
                                    elementObj = foundElementObjs.ElementAtOrDefault(index);
                                }
                                else
                                {
                                    elementObj = foundElementObjs?.FirstOrDefault();
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("FindElement(By.ClassName(" + _ClassItem.ClassName, ex));
                    }
                }
            }
            if (elementObj == null)
            {
                if (!string.IsNullOrWhiteSpace(_TagItem?.TagName))
                {
#if DEBUG
                    if (_TagItem.TagName == "td")
                    {

                    }
                    if (_TagItem.Text == "Compare Images")
                    {

                    }
#endif

                    try
                    {
                        var foundElementObjs = searchCtx.FindElements(By.TagName(_TagItem.TagName))?.ToList();

                        foundElementObjs = foundElementObjs.Where(ite => ite.Displayed)?.ToList();

                        if (foundElementObjs?.Count > 0)
                        {
#if DEBUG
#endif
                            if (_TagItem.Attributes?.Count > 0)
                            {
                                foreach (var attrObj in _TagItem.Attributes)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => attrObj.ValidPredicate(ite.GetDomAttribute(attrObj.Name)))
                                    ?.ToList();
                                }
                            }
                            if (_TagItem.Values?.Count > 0)
                            {
                                var values = _TagItem.Values.Where(ite => !string.IsNullOrWhiteSpace(ite)).ToList();
                                if (values?.Count > 0)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => values.Contains(ite.GetAttribute("value")?.Trim(), StringComparer.InvariantCultureIgnoreCase))
                                    ?.ToList();
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(_TagItem.Value))
                            {
                                foundElementObjs = foundElementObjs
                                ?.Where(ite => ite.GetAttribute("value")?.Trim()?.Equals(_TagItem.Value, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ?.ToList();
                            }

                            if (_TagItem.Texts?.Count > 0)
                            {
                                var texts = _TagItem.Texts.Where(ite => !string.IsNullOrWhiteSpace(ite)).ToList();
                                if (texts?.Count > 0)
                                {
                                    foundElementObjs = foundElementObjs
                                    ?.Where(ite => texts.Contains(ite.Text, StringComparer.InvariantCultureIgnoreCase))
                                    ?.ToList();
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(_TagItem.Text))
                            {
                                foundElementObjs = foundElementObjs
                                ?.Where(ite => ite.Text?.Trim()?.Equals(_TagItem.Text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                                ?.ToList();
                            }

                            if (foundElementObjs?.Count > 0)
                            {
                                int index = _TagItem.Index ?? foundElementObjs.Count;
                                if (index < foundElementObjs.Count)
                                {
                                    elementObj = foundElementObjs.ElementAtOrDefault(index);
                                }
                                else
                                {
                                    elementObj = foundElementObjs?.FirstOrDefault();
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("FindElement(By.TagName(" + _TagItem.TagName, ex));
                    }
                }
            }

            return elementObj;
        }
        public ElementProps() { }
        public ElementProps(string id, string name = null, string parentID = null, string iFrameID = null, bool? switchToNewWindow = null)
        {
            _ID = id;
            _Name = name;
            _ParentID = parentID;
            _IFrameID = iFrameID;
            _SwitchToNewWindow = switchToNewWindow;
        }
    }
}
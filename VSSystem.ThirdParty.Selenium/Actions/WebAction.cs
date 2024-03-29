using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using VSSystem.ThirdParty.Selenium.Define;
using VSSystem.ThirdParty.Selenium.Extensions;

namespace VSSystem.ThirdParty.Selenium.Actions
{
    [Newtonsoft.Json.JsonObject(ItemNullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public class WebAction
    {
        protected string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        protected string _Type;
        public string Type { get { return _Type; } set { _Type = value; } }
        [JsonIgnore]
        EActionType EType
        {
            get
            {
                EActionType result = EActionType.Undefine;
                if (!string.IsNullOrWhiteSpace(_Type))
                {
                    Enum.TryParse(_Type, true, out result);
                }
                return result;
            }
        }

        public WebAction() { }
        public WebAction(EActionType actionType = EActionType.Undefine) { _Type = actionType.ToString(); }
        public WebAction(string name, EActionType actionType = EActionType.Undefine)
        {
            if (actionType != EActionType.Undefine)
            {
                _Type = actionType.ToString();
            }

            _Name = name;
            _Actions = null;
        }

        #region Element

        #region Identity
        ElementProps _Props;
        public ElementProps Props { get { return _Props; } set { _Props = value; } }
        bool? _Click;
        public bool? Click { get { return _Click; } set { _Click = value; } }
        bool? _RightClick;
        public bool? RightClick { get { return _RightClick; } set { _RightClick = value; } }
        bool? _DoubleClick;
        public bool? DoubleClick { get { return _DoubleClick; } set { _DoubleClick = value; } }
        bool? _ClickAndHold;
        public bool? ClickAndHold { get { return _ClickAndHold; } set { _ClickAndHold = value; } }
        bool? _MouseIn;
        public bool? MouseIn { get { return _MouseIn; } set { _MouseIn = value; } }
        bool? _ScrollTo;
        public bool? ScrollTo { get { return _ScrollTo; } set { _ScrollTo = value; } }
        #endregion

        #region Actions props

        bool? _ShiftKey;
        public bool? ShiftKey { get { return _ShiftKey; } set { _ShiftKey = value; } }
        bool? _ControlKey;
        public bool? ControlKey { get { return _ControlKey; } set { _ControlKey = value; } }
        bool? _AltKey;
        public bool? AltKey { get { return _AltKey; } set { _AltKey = value; } }

        #endregion

        #region Key Press
        List<string> _PressKeys;
        public List<string> PressKeys { get { return _PressKeys; } set { _PressKeys = value; } }
        #endregion

        #endregion

        #region Screen Shot props
        string _FolderPath;
        public string FolderPath { get { return _FolderPath; } set { _FolderPath = value; } }
        string _FileName;
        public string FileName { get { return _FileName; } set { _FileName = value; } }

        #endregion

        double? _DelaySeconds;
        public double? DelaySeconds { get { return _DelaySeconds; } set { _DelaySeconds = value; } }
        protected string _Url;
        public string Url
        {
            get
            {
                try
                {
                    return _Url;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            set { _Url = value; }
        }
        protected List<WebAction> _Actions;
        public List<WebAction> Actions { get { return _Actions; } set { _Actions = value; } }
        protected string _Title;
        public string Title { get { return _Title; } set { _Title = value; } }
        WebAction _OnValidateInvalid;
        public WebAction OnValidateInvalid { get { return _OnValidateInvalid; } set { _OnValidateInvalid = value; } }
        protected string _previousWindowHandle;

        public virtual bool Execute(IWebDriver driver, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            bool result = true;
            DateTime now = DateTime.Now;
            try
            {
                double delayMiliseconds = 50;
                if (_DelaySeconds > 0)
                {
                    delayMiliseconds = Convert.ToInt32(_DelaySeconds * 1000);
                }
                if (delayMiliseconds > 0)
                {
                    Thread.Sleep(System.TimeSpan.FromMilliseconds(delayMiliseconds));
                }



                if (EType == EActionType.ScreenShot)
                {

                    try
                    {
                        if (_Props?.SwitchToNewWindow ?? false)
                        {
                            try
                            {
                                _previousWindowHandle = driver.CurrentWindowHandle;
                                var originalSize = driver.Manage().Window.Size;
                                var lastWindow = driver.WindowHandles.LastOrDefault();
                                if (!string.IsNullOrWhiteSpace(lastWindow))
                                {
                                    driver = driver.SwitchTo().Window(lastWindow);
                                    driver.Manage().Window.Size = originalSize;
                                }

                            }
                            catch (Exception ex)
                            {
                                errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                            }
                        }
                        string folderPath = _FolderPath;
                        if (string.IsNullOrWhiteSpace(folderPath))
                        {
                            folderPath = $"{Directory.GetCurrentDirectory()}/screenshots";
                        }
                        string fileName = _FileName;
                        if (string.IsNullOrWhiteSpace(fileName))
                        {
                            fileName = Guid.NewGuid().ToString().ToLower();
                        }

                        FileInfo file = new FileInfo($"{folderPath}/{fileName}.png");
                        if (!file.Directory.Exists)
                        {
                            file.Directory.Create();
                        }

                        var screenShotObj = ((ITakesScreenshot)driver).GetScreenshot();
                        var screenshotBytes = screenShotObj.AsByteArray;
                        try
                        {
                            screenshotBytes = ImageExtension.ConvertImage(screenshotBytes, "jpg");
                            file = new FileInfo($"{folderPath}/{fileName}.jpg");
                        }
                        catch { }
                        File.WriteAllBytes(file.FullName, screenshotBytes);

                        if (_Props?.SwitchToNewWindow ?? false)
                        {
                            try
                            {
                                if (_Props?.CloseWindow ?? false)
                                {
                                    driver.Close();
                                }

                                if (!string.IsNullOrWhiteSpace(_previousWindowHandle))
                                {
                                    driver = driver.SwitchTo().Window(_previousWindowHandle);
                                }
                                else
                                {
                                    var previousWindowHandle = driver.WindowHandles[driver.WindowHandles.Count - 1];
                                    if (!string.IsNullOrWhiteSpace(previousWindowHandle))
                                    {
                                        driver = driver.SwitchTo().Window(previousWindowHandle);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Screenshot exception.", ex));
                    }


                }

                if (!string.IsNullOrWhiteSpace(_Url))
                {
                    if (driver.Url?.Equals(_Url) ?? false)
                    {
                        return true;
                    }
                    driver.Url = _Url;
                }

                if (_Props != null)
                {
                    result = _ExecuteElement(driver, debugLogAction, errorLogAction);

                    if (EType == EActionType.Validate)
                    {
                        _OnValidateInvalid?.Execute(driver, debugLogAction, errorLogAction);
                        return result;
                    }
                }
                else
                {
                    if (_PressKeys?.Count > 0)
                    {
                        result = _KeyAction(driver, debugLogAction, errorLogAction);
                        if (EType == EActionType.Validate)
                        {
                            _OnValidateInvalid?.Execute(driver, debugLogAction, errorLogAction);
                            return result;
                        }
                    }
                    else
                    {
                        result = _ExecuteTitle(driver, debugLogAction, errorLogAction);
                        if (EType == EActionType.Validate)
                        {
                            _OnValidateInvalid?.Execute(driver, debugLogAction, errorLogAction);
                            return result;
                        }
                    }
                }

                if (_Actions?.Count > 0)
                {
                    if (_Props?.SwitchToNewWindow ?? false)
                    {
                        try
                        {
                            _previousWindowHandle = driver.CurrentWindowHandle;
                            var originalSize = driver.Manage().Window.Size;
                            var lastWindow = driver.WindowHandles.LastOrDefault();
                            if (!string.IsNullOrWhiteSpace(lastWindow))
                            {
                                driver = driver.SwitchTo().Window(lastWindow);
                                driver.Manage().Window.Size = originalSize;
                            }

                        }
                        catch (Exception ex)
                        {
                            errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                        }
                    }

                    foreach (var actionObj in _Actions)
                    {
                        try
                        {
                            var actionResult = actionObj.Execute(driver, debugLogAction, errorLogAction);
                            if (!actionResult)
                            {
                                result = false;
                                if (actionObj.EType == EActionType.Validate)
                                {
                                    break;
                                }
                            }
                        }
                        catch { }
                    }

                    if (_Props?.SwitchToNewWindow ?? false)
                    {
                        try
                        {
                            if (_Props?.CloseWindow ?? false)
                            {
                                driver.Close();
                            }

                            if (!string.IsNullOrWhiteSpace(_previousWindowHandle))
                            {
                                driver = driver.SwitchTo().Window(_previousWindowHandle);
                            }
                            else
                            {
                                var previousWindowHandle = driver.WindowHandles[driver.WindowHandles.Count - 1];
                                if (!string.IsNullOrWhiteSpace(previousWindowHandle))
                                {
                                    driver = driver.SwitchTo().Window(previousWindowHandle);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorLogAction?.Invoke(ex);
            }
            return result;
        }

        protected virtual bool _KeyAction(IWebDriver driver, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            try
            {
                if (_Props?.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        _previousWindowHandle = driver.CurrentWindowHandle;
                        var originalSize = driver.Manage().Window.Size;
                        var lastWindow = driver.WindowHandles.LastOrDefault();
                        if (!string.IsNullOrWhiteSpace(lastWindow))
                        {
                            driver = driver.SwitchTo().Window(lastWindow);
                            driver.Manage().Window.Size = originalSize;
                        }

                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }
                if (!string.IsNullOrWhiteSpace(_Props?.IFrameID))
                {
#if DEBUG
                    if (_Props.IFrameID == "iframeNotifyInfo")
                    {

                    }
#endif
                    try
                    {
                        driver = driver.SwitchTo().Frame(_Props.IFrameID);
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception($"Change IFrame {_Props.IFrameID} exception.", ex));
                    }
                }

                var actionObj = new OpenQA.Selenium.Interactions.Actions(driver);
                if (_ControlKey ?? false)
                {
                    actionObj = actionObj.KeyDown(Keys.Control);
                }
                if (_ShiftKey ?? false)
                {
                    actionObj = actionObj.KeyDown(Keys.Shift);
                }
                if (_AltKey ?? false)
                {
                    actionObj = actionObj.KeyDown(Keys.Alt);
                }

                foreach (var pressKey in _PressKeys)
                {
                    actionObj = actionObj.KeyDown(pressKey);
                }

                if (_ControlKey ?? false)
                {
                    actionObj = actionObj.KeyUp(Keys.Control);
                }
                if (_ShiftKey ?? false)
                {
                    actionObj = actionObj.KeyUp(Keys.Shift);
                }
                if (_AltKey ?? false)
                {
                    actionObj = actionObj.KeyUp(Keys.Alt);
                }

                foreach (var pressKey in _PressKeys)
                {
                    actionObj = actionObj.KeyUp(pressKey);
                }

                actionObj.Build().Perform();


                if (!string.IsNullOrWhiteSpace(_Props?.IFrameID))
                {
                    try
                    {
                        driver = driver.SwitchTo().DefaultContent();
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("SwitchTo.ActiveElement exception.", ex));
                    }
                }

                if (_Props?.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(_previousWindowHandle))
                        {
                            driver = driver.SwitchTo().Window(_previousWindowHandle);
                        }
                        else
                        {
                            var previousWindowHandle = driver.WindowHandles[driver.WindowHandles.Count - 1];
                            if (!string.IsNullOrWhiteSpace(previousWindowHandle))
                            {
                                driver = driver.SwitchTo().Window(previousWindowHandle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }

            }
            catch (Exception ex)
            {
                errorLogAction?.Invoke(new Exception("_KeyAction exception.", ex));
            }
            return true;
        }
        protected virtual bool _ExecuteTitle(IWebDriver driver, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            bool result = true;
            try
            {
                if (_Props?.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        _previousWindowHandle = driver.CurrentWindowHandle;
                        var originalSize = driver.Manage().Window.Size;
                        var lastWindow = driver.WindowHandles.LastOrDefault();
                        if (!string.IsNullOrWhiteSpace(lastWindow))
                        {
                            driver = driver.SwitchTo().Window(lastWindow);
                            driver.Manage().Window.Size = originalSize;
                        }

                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }
                if (!string.IsNullOrWhiteSpace(_Props?.IFrameID))
                {
#if DEBUG
                    if (_Props.IFrameID == "iframeNotifyInfo")
                    {

                    }
#endif
                    try
                    {
                        driver = driver.SwitchTo().Frame(_Props.IFrameID);
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change IFrame exception.", ex));
                    }
                }

                if (EType == EActionType.Wait)
                {
                    int waitTime = 500;
                    if (!string.IsNullOrWhiteSpace(_Title))
                    {
                        string title = driver.Title;
                        while (!title?.Equals(_Title, StringComparison.InvariantCultureIgnoreCase) ?? false)
                        {
                            Thread.Sleep(waitTime);
                        }
                    }
                }
                else if (EType == EActionType.Validate)
                {
                    if (!string.IsNullOrWhiteSpace(_Title))
                    {
                        string title = driver.Title;
                        result = title?.Equals(_Title, StringComparison.InvariantCultureIgnoreCase) ?? false;
                    }

                }

                if (!string.IsNullOrWhiteSpace(_Props?.IFrameID))
                {
                    try
                    {
                        driver = driver.SwitchTo().DefaultContent();
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("SwitchTo.ActiveElement exception.", ex));
                    }
                }

                if (_Props?.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(_previousWindowHandle))
                        {
                            driver = driver.SwitchTo().Window(_previousWindowHandle);
                        }
                        else
                        {
                            var previousWindowHandle = driver.WindowHandles[driver.WindowHandles.Count - 1];
                            if (!string.IsNullOrWhiteSpace(previousWindowHandle))
                            {
                                driver = driver.SwitchTo().Window(previousWindowHandle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }
            }
            catch (Exception ex)
            {
                errorLogAction?.Invoke(new Exception("_ExecuteTitle exception.", ex));
            }
            return result;
        }
        bool _ExecuteElement(IWebDriver driver, Action<string> debugLogAction = null, Action<Exception> errorLogAction = null)
        {
            bool result = true;
            try
            {
                if (_Props.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        _previousWindowHandle = driver.CurrentWindowHandle;
                        var originalSize = driver.Manage().Window.Size;
                        var lastWindow = driver.WindowHandles.LastOrDefault();
                        if (!string.IsNullOrWhiteSpace(lastWindow))
                        {
                            driver = driver.SwitchTo().Window(lastWindow);
                            driver.Manage().Window.Size = originalSize;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }
                if (!string.IsNullOrWhiteSpace(_Props.IFrameID))
                {
#if DEBUG
                    if (_Props.IFrameID == "iframeNotification")
                    {

                    }

#endif
                    try
                    {
                        driver = driver.SwitchTo().Frame(_Props.IFrameID);
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change IFrame exception.", ex));
                    }
                }



                var elementObj = Props.GetWebElement(driver, debugLogAction, errorLogAction);
                if (elementObj != null)
                {
                    if (EType == EActionType.Wait)
                    {
                        int waitTime = 500;
                        if (_Props.Displayed != null)
                        {
                            while (elementObj.Displayed != _Props.Displayed)
                            {
                                Thread.Sleep(waitTime);
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(_Props.Text))
                        {
                            string text = elementObj.Text;
                            while (elementObj.Text.Equals(text, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Thread.Sleep(waitTime);
                            }
                        }
                    }
                    else if (EType == EActionType.Validate)
                    {
                        if (_Props.Displayed != null)
                        {
                            if (elementObj.Displayed != _Props.Displayed)
                            {
                                result = false;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(_Props.Text))
                        {
                            if (!elementObj.Text?.Equals(_Props.Text) ?? false)
                            {
                                result = false;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(_Props.Value))
                        {
                            if (!elementObj.GetAttribute("value")?.Equals(_Props.Value) ?? false)
                            {
                                result = false;
                            }
                        }

                    }

                    // if (elementObj.Displayed)
                    {

                        if ((_ControlKey ?? false) || (_ShiftKey ?? false) || (_AltKey ?? false) || _PressKeys?.Count > 0)
                        {
                            var actionObj = new OpenQA.Selenium.Interactions.Actions(driver);
                            if (_ControlKey ?? false)
                            {
                                actionObj = actionObj.KeyDown(Keys.Control);
                            }
                            if (_ShiftKey ?? false)
                            {
                                actionObj = actionObj.KeyDown(Keys.Shift);
                            }
                            if (_AltKey ?? false)
                            {
                                actionObj = actionObj.KeyDown(Keys.Alt);
                            }

                            if (_PressKeys?.Count > 0)
                            {
                                foreach (var pressKey in _PressKeys)
                                {
                                    actionObj = actionObj.KeyDown(pressKey);
                                }
                            }


                            if (_MouseIn ?? false)
                            {
                                actionObj = actionObj.MoveToElement(elementObj);
                            }
                            if (_ScrollTo ?? false)
                            {
                                actionObj = actionObj.ScrollToElement(elementObj);
                            }
                            if (_Props.Checked != null)
                            {
                                if (elementObj.Selected != _Props.Checked)
                                {
                                    actionObj = actionObj.Click(elementObj);
                                }
                            }
                            if (_Click ?? false)
                            {
                                actionObj = actionObj.Click(elementObj);
                            }
                            if (_RightClick ?? false)
                            {
                                actionObj = actionObj.ContextClick(elementObj);
                            }
                            if (_DoubleClick ?? false)
                            {
                                actionObj = actionObj.DoubleClick(elementObj);
                            }

                            if (_ClickAndHold ?? false)
                            {
                                actionObj = actionObj.ClickAndHold(elementObj);

                            }

                            if (_ControlKey ?? false)
                            {
                                actionObj = actionObj.KeyUp(Keys.Control);
                            }
                            if (_ShiftKey ?? false)
                            {
                                actionObj = actionObj.KeyUp(Keys.Shift);
                            }
                            if (_AltKey ?? false)
                            {
                                actionObj = actionObj.KeyUp(Keys.Alt);
                            }

                            if (_PressKeys?.Count > 0)
                            {
                                foreach (var pressKey in _PressKeys)
                                {
                                    actionObj = actionObj.KeyUp(pressKey);
                                }
                            }
                            actionObj.Build().Perform();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(_Props.Value))
                            {
                                if (_Props.EType == EElementType.Select)
                                {
                                    if (!string.IsNullOrWhiteSpace(_Props.Value))
                                    {
                                        try
                                        {
                                            new SelectElement(elementObj).SelectByValue(_Props.Value);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    elementObj.Clear();
                                    elementObj.SendKeys(_Props.Value);
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(_Props.Text))
                            {
                                if (_Props.EType == EElementType.Select)
                                {
                                    try
                                    {
                                        new SelectElement(elementObj).SelectByText(_Props.Text);
                                    }
                                    catch //(Exception ex)
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        elementObj.Click();
                                        elementObj.Clear();
                                        elementObj.SendKeys(_Props.Text);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            new OpenQA.Selenium.Interactions.Actions(driver)
                                            .MoveToElement(elementObj, 1, 1)
                                            .Click(elementObj)
                                            .SendKeys(_Props.Text)
                                            .Perform();
                                        }
                                        catch { }
                                    }
                                }
                            }
                            if (_MouseIn ?? false)
                            {
                                new OpenQA.Selenium.Interactions.Actions(driver)
                                .MoveToElement(elementObj)
                                .Perform();
                            }
                            if (_ScrollTo ?? false)
                            {
                                new OpenQA.Selenium.Interactions.Actions(driver)
                                .ScrollToElement(elementObj)
                                .Perform();
                            }
                            if (_Props.Checked != null)
                            {
                                if (elementObj.Selected != _Props.Checked)
                                {
                                    elementObj.Click();
                                }
                            }
                            if (_Click ?? false)
                            {
                                try
                                {
                                    elementObj.Click();
                                }
                                catch
                                {
                                    try
                                    {
                                        new OpenQA.Selenium.Interactions.Actions(driver)
                                        .MoveToElement(elementObj, 1, 1)
                                        .Click(elementObj)
                                        .Perform();
                                    }
                                    catch { }
                                }
                            }
                            if (_RightClick ?? false)
                            {
                                try
                                {
                                    new OpenQA.Selenium.Interactions.Actions(driver)
                                    .MoveToElement(elementObj, 1, 1)
                                    .ContextClick(elementObj)
                                    .Perform();
                                }
                                catch { }
                            }
                            if (_DoubleClick ?? false)
                            {
                                new OpenQA.Selenium.Interactions.Actions(driver)
                                .DoubleClick(elementObj)
                                .Perform();
                            }

                            if (_ClickAndHold ?? false)
                            {
                                new OpenQA.Selenium.Interactions.Actions(driver)
                                .ClickAndHold(elementObj)
                                .Perform();
                            }
                        }
                        if (_Props.Actions?.Count > 0)
                        {
                            foreach (var actionObj in _Props.Actions)
                            {
                                try
                                {
                                    if (actionObj.Props != null)
                                    {
                                        actionObj.Props.ParentElement = elementObj;
                                    }

                                    actionObj.Execute(driver, debugLogAction, errorLogAction);
                                }
                                catch (Exception ex)
                                {
                                    errorLogAction?.Invoke(new Exception("_Props.Actions execute exception.", ex));
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (EType == EActionType.Wait)
                    {
                        int waitTime = 500;
                        if (!string.IsNullOrWhiteSpace(_Title))
                        {
                            string title = driver.Title;
                            while (!title?.Equals(_Title, StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                Thread.Sleep(waitTime);
                            }
                        }
                    }
                    else if (EType == EActionType.Validate)
                    {
                        if (!string.IsNullOrWhiteSpace(_Title))
                        {
                            string title = driver.Title;
                            result = title?.Equals(_Title, StringComparison.InvariantCultureIgnoreCase) ?? false;
                        }

                    }
                }
                if (!string.IsNullOrWhiteSpace(_Props.IFrameID))
                {
                    try
                    {
                        driver = driver.SwitchTo().DefaultContent();
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("SwitchTo.ActiveElement exception.", ex));
                    }
                }

                if (_Props.SwitchToNewWindow ?? false)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(_previousWindowHandle))
                        {
                            driver = driver.SwitchTo().Window(_previousWindowHandle);
                        }
                        else
                        {
                            var previousWindowHandle = driver.WindowHandles[driver.WindowHandles.Count - 1];
                            if (!string.IsNullOrWhiteSpace(previousWindowHandle))
                            {
                                driver = driver.SwitchTo().Window(previousWindowHandle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogAction?.Invoke(new Exception("Change new window exception.", ex));
                    }
                }

            }
            catch (Exception ex)
            {
                errorLogAction?.Invoke(new Exception("_ExecuteElement exception.", ex));
            }

            return result;
        }


    }
}
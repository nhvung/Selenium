using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testselenium
{
    class Program
    {
        static Dictionary<string, bool> _validateResult;
        async static Task Main(string[] args)
        {
            // string checkUrl = $"https://14.161.7.248:4431/ballisticsearch";
            string checkUrl = $"https://sandbox.evidenceiq.com/biq";
            var client = new VSSystem.ThirdParty.Selenium.Client();

            var sections = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.Section>()
                {
                    new VSSystem.ThirdParty.Selenium.Actions.Section("test login"){

                        Actions = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
                            new VSSystem.ThirdParty.Selenium.Actions.NavigateAction(checkUrl),
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction() {
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ID="txtUserName",
                                Value = "leu.vung1"
                                }

                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction() {
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ID="txtPassword",
                                Value = "Evidence2!"
                                }

                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction() {
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
 ID="btnOk",

                                },
                                Click = true

                            },
                            new VSSystem.ThirdParty.Selenium.Actions.NavigateAction($"{checkUrl}/GUI/Home.aspx")
                            {
                                //DelaySeconds = 5
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                Type = "select",
                                ID="ctl00_AgencySelectionControl_cboAgencySelection",
                                Text="LEA_Vung"
                                },
                                DelaySeconds = 1

                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ScreenShotAction(){
                                DelaySeconds = 1,
                                FileName = "step-1-login-success"
                            }
                        },

                    },
                    new VSSystem.ThirdParty.Selenium.Actions.Section("test view gallery"){

                        Actions = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
                           new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ID="dvBallistics",

                                },
Click=true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                            {
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ClassItem=new VSSystem.ThirdParty.Selenium.Actions.ClassProps(){
                                    ClassName = "icon_function",
                                    Index = 2
                                },
                                },

                                DelaySeconds = 1,
                                Click=true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
IFrameID="GCTIFrame",
                                ID="btnCancel",
                                },
                                DelaySeconds = 2,

                                Click=true
                            },

//                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
// ID="ctl00_ContentPlaceHolder1_ucNameFilter_txtCaseNumber",
//                                 Value="new tool"
//                                 },
//                                 DelaySeconds = 1,

//                             },
//                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  ClassItem=new VSSystem.ThirdParty.Selenium.Actions.ClassProps("ms-options-wrap", 0),
//                                 },
//                                 DelaySeconds = 1,

//                                 Click = true
//                             },
//                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  XPath="//*[@id=\"box-filter\"]/div[1]/div[2]/div/table/tbody/tr[7]/td[2]/div/div/ul/li[1]/ul/li[6]/label",
//                                 },
//                                 DelaySeconds = 1,

//                                Click=true
//                             },
//                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  Type = "select",
//                                 ID="cboAgencyScope",
//                                 Value="-3"
//                                 },
//                                 DelaySeconds = 1,

//                             },
//                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props =new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  ParentID="list-share",
//                                 ID="chk-all-share",
//                                 Checked = false
//                                 },
//                                 DelaySeconds = 2,

//                             },
//                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  ParentID="list-share",
//                                  TagItem=new VSSystem.ThirdParty.Selenium.Actions.TagProps("input", 1),
//                                 Checked = true,
//                                 },
//                                 DelaySeconds = 1,

//                             },
//                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
// ParentID="list-share",
//                                  TagItem=new VSSystem.ThirdParty.Selenium.Actions.TagProps("input", 2),
//                                 Checked = true,
//                                 },

//                             },
//                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
// ID="chk-all-state",
//                                 Checked = false
//                                 },
//                                 DelaySeconds = 2,

//                             },
//                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
//  ID="state6",
//                                 Checked = true
//                                 },
//                                 DelaySeconds = 1,

//                             },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                            {
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ClassItem=new VSSystem.ThirdParty.Selenium.Actions.ClassProps(){
                                    ClassName = "button100_green",
                                    Index = 0
                                },
                                },
                                DelaySeconds = 1,

                                Click=true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
ID= "txtCaseNumber",
                                IFrameID="GCTIFrame",
                                Value = "Test Selenium"
                                },
                                DelaySeconds = 1,

                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
IFrameID="GCTIFrame",
                                ID="btnContinue",
                                },

                                Click=true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction(){
    Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
        ClassItem =new VSSystem.ThirdParty.Selenium.Actions.ClassProps("ajax_overlay", 1),

    },
    Displayed = false,
},
                        },
                        ValidateActions = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.IAction>()
                        {
                            new VSSystem.ThirdParty.Selenium.Actions.ElementValidateAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
 ID = "resultview",
                                },

                                Displayed = true,
                            }
                        },
                        ScreenShot = new VSSystem.ThirdParty.Selenium.Actions.ScreenShotAction(){
                            DelaySeconds = 1,
                            FileName = "step-2-view-gallery"
                        }
                    },
                    new VSSystem.ThirdParty.Selenium.Actions.Section("test search 1n"){

                        Actions = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
                                    TagItem = new VSSystem.ThirdParty.Selenium.Actions.TagProps("span", text: "9mm Luger / 9mm Parabellum / 9mm Luger +P / 9x19mm Parabellum", index: 1),
                                    ParentID = "resultview"
                                },
                                Click = true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
                                Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
                                    ID="ctl00_ContentPlaceHolder1_btnSearchFace",
                                },

                                Click = true,
                                DelaySeconds = 3
                            },

                            new VSSystem.ThirdParty.Selenium.Actions.NavigateWaitingAction("https://sandbox.evidenceiq.com/biq/GUI/QuickSearch.aspx",(driverUrl, url)=>driverUrl.StartsWith(url,StringComparison.InvariantCultureIgnoreCase))
                            {
                                DelaySeconds = 10
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                    ID = "ctl00_ContentPlaceHolder1_btnQuickSearch",
                                },
                                DelaySeconds = 5,

                                Click = true
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                    ClassItem = new VSSystem.ThirdParty.Selenium.Actions.ClassProps("ajax_overlay", 0),

                                },
                                Displayed = false,
                                DelaySeconds = 3
                            },
                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                             {
                                 Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                 {
                                     IFrameID = "GCTIFrame",
                                     ID = "btnContinue",
                                 },

                                 Click = true
                             },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                    ClassItem = new VSSystem.ThirdParty.Selenium.Actions.ClassProps("ajax_overlay", 0),
                                },
                                Displayed = false,
                            },
                            new VSSystem.ThirdParty.Selenium.Actions.ElementAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                    ParentID = "pAlertWin",
                                    ID = "btnControl_0",
                                },
                                DelaySeconds = 1,
                                Click = true
                            },

                            new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction()
                            {
                                Props = new VSSystem.ThirdParty.Selenium.Actions.ElementProps()
                                {
                                    ClassItem = new VSSystem.ThirdParty.Selenium.Actions.ClassProps("ajax_overlay", 1),
                                },
                                Displayed = false,
                            },

// new VSSystem.ThirdParty.Selenium.Actions.NavigateAction() { DelaySeconds = 5 },

                        },
                        ScreenShot = new VSSystem.ThirdParty.Selenium.Actions.ScreenShotAction(){
                            DelaySeconds = 1,
                            FileName = "step-3-run-search-1n"
                        },

//                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
// SwitchToNewWindow = true,
//                                 TagItem = new VSSystem.ThirdParty.Selenium.Actions.TagProps("button", text: "Wait"),
//                                 },
//                                 DelaySeconds = 20,

//                                 Click = true
//                             },
                        
//                         WaitingActions = new List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
//                             new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction(){
//                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
// ID = "loading-msg",
// SwitchToNewWindow = true,
//                                 },

//                                 Displayed = false,

//                                 DelaySeconds = 5
//                             }
//                         },
//                         ScreenShot = new VSSystem.ThirdParty.Selenium.Actions.ScreenShotAction(){
//                             DelaySeconds = 1,
//                             FileName = "step-3-run-csa"
//                         }
                    },
//                     new VSSystem.ThirdParty.Selenium.Actions.Section("test csa", (name,correct)=> Console.WriteLine($"{name} is correct.")){

            //                         RequestActions = new System.Collections.Generic.List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
            //                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
            //                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
            // ID="chkSelectAll",
            //                                 },
            //                                 DelaySeconds = 1,

            //                                 Click = true
            //                             },
            //                             new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
            //                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
            // ID="ctl00_ContentPlaceHolder1_btnAnalysisReport",
            //                                 },

            //                                 Click = true
            //                             },
            //                              new VSSystem.ThirdParty.Selenium.Actions.ElementAction(){
            //                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
            // SwitchToNewWindow = true,
            //                                 TagItem = new VSSystem.ThirdParty.Selenium.Actions.TagProps("button", text: "Wait"),
            //                                 },
            //                                 DelaySeconds = 20,

            //                                 Click = true
            //                             },
            //                         },
            //                         WaitingActions = new List<VSSystem.ThirdParty.Selenium.Actions.IAction>(){
            //                             new VSSystem.ThirdParty.Selenium.Actions.ElementWaitingAction(){
            //                                 Props=new VSSystem.ThirdParty.Selenium.Actions.ElementProps(){
            // ID = "loading-msg",
            // SwitchToNewWindow = true,
            //                                 },

            //                                 Displayed = false,

            //                                 DelaySeconds = 5
            //                             }
            //                         },
            //                         ScreenShot = new VSSystem.ThirdParty.Selenium.Actions.ScreenShotAction(){
            //                             DelaySeconds = 1,
            //                             FileName = "step-3-run-csa"
            //                         }
            //                     },
                };

            var taskParams1Obj = new VSSystem.ThirdParty.Selenium.Actions.ActionTask("Test BIQ web chrome")
            {
                IsIncognito = true,
                Browser = "chrome",
                Sections = sections
            };

            var taskParams2Obj = new VSSystem.ThirdParty.Selenium.Actions.ActionTask("Test BIQ web firefox")
            {
                IsIncognito = true,
                Browser = "firefox",
                Sections = (new[]
                { sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                sections,
                }).SelectMany(ite => ite)
                .ToList()
            };

            var taskParams3Obj = new VSSystem.ThirdParty.Selenium.Actions.ActionTask("Test BIQ web edge")
            {
                IsIncognito = true,
                Browser = "edge",
                Sections = sections
            };

            client.Execute(new VSSystem.ThirdParty.Selenium.Actions.ActionTask[]{
                // taskParams1Obj,
            taskParams2Obj,
            // taskParams3Obj,
            },
                                errorLogAction: ex =>
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine(ex.StackTrace);
                                    if (ex.InnerException != null)
                                    {
                                        Console.WriteLine(ex.InnerException.Message);
                                        Console.WriteLine(ex.InnerException.StackTrace);
                                    }

                                }
                                );
        }
    }
}
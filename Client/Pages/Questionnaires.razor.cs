using AKSoftware.Localization.MultiLanguages;
using Blazored.TextEditor;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace CandidatePortal.Client.Pages
{
    public partial class Questionnaires
    {

        //Enum

        // 0 = String;
        // 1 = Number;
        // 2 = Float;
        // 3 = Date;
        // 4 = Time;
        // 5 = Multiline
        // 6 = CheckBox
        // 7 = Radio
        // 8 = DropDown


        #region Declaretion
        public BlazoredTextEditor QuillHtml { get; set; } = new BlazoredTextEditor();

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Parameter]
        public string NCandidateId { get; set; } = "";

        [Parameter]
        public long ApplicationNumber { get; set; } = 0;

        [Inject]
        public IApplicationQuestionaires _objRepo { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public string SelectedOption { get; set; }

        DateTime StartDateTime = DateTime.Now; // here

        public Boolean IsStart = true;
        bool IsLoding = false;

        // private IJSObjectReference _jsModule;

        static string AppsCommunicationLink = "";
        public int QuesCount = 0;
        public int QuesCountstr = 0;

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("appliedjob", href: "appliedjob", icon: Icons.Material.Filled.AdsClick),
            new BreadcrumbItem("Application Quiestionnaires", href: null , icon: Icons.Material.Filled.AdsClick, disabled: true)
        };

        //public string ImgUrl { get; set; }
        //public string PageTitle { get; set; } = "";
        //private string lblResumeName = "";
        //private string htmltext = "";

        public ApplicationQuestionnaireDTO AppsQuesMainModel = new ApplicationQuestionnaireDTO();

        public ApplicationQuestionnaireLineDTO AppsQuesModel = new ApplicationQuestionnaireLineDTO();

        public List<ApplicationQuestionnaireLineDTO> obj_AppsQuesModelList = new List<ApplicationQuestionnaireLineDTO>();

        public ApplicationCommunicationDocumentAddDTO obj_AppsDocModel = new ApplicationCommunicationDocumentAddDTO();

        public ResponseStatusData Obj_Response = new ResponseStatusData();


        List<ApplicationQuestionnaireLineDTO> obj_ApplicationQuestionnaireLine = new List<ApplicationQuestionnaireLineDTO>();

        //public MarkupStringSanitized MarkupStringSanitized = new MarkupStringSanitized();


        JobAppliedDTO obj_JobAppliedDTO = new JobAppliedDTO();


        public EventCallback<string> SetTitleEvent => EventCallback.Factory.Create<string>(this, SeActiveTime);



        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        string SCandidateEmail = "";

        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        public bool isValid = true;
        private bool IsLoaderShow = false;

        public bool Question_CheckId { get; set; } = true;
        public string Question_RadioId { get; set; } = "";
        public long Question_NumberId = 0;
        public float Question_FloatId = 0;
        TimeSpan? Question_TimeId = new TimeSpan(00, 45, 00);
        DateTime? Question_DateId = DateTime.Today;


        public string customstr { get; set; } = CustomStringToBoolConverter.FalseString;


        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        #endregion

        #region On Initilize
        protected override async Task OnInitializedAsync()
        {
            // _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/downloadScript.js"); //
            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                IsLoding = true;
                IsLoaderShow = false;
                InitializationForm();
                AppsQuesMainModel.ApplicationQuestionnaireLine.Add(new ApplicationQuestionnaireLineDTO());

                await GetAppsQuestionnariesAsync();
                IsLoding = false;
            }
            else
            {
                signOut();
            }
        }
        #endregion

        public void InitializationForm()
        {
            AppsQuesMainModel = new ApplicationQuestionnaireDTO
            {
                ApplicationNumber = 0,
                Number = 0,
                IsSynced = 0,
            };
        }

        public async Task GetAppsQuestionnariesAsync()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetAppQuestionnaireByAppsNumberAsync(ApplicationNumber);
            if (Obj_Response.IsSuccess)
            {
                AppsQuesMainModel.ApplicationQuestionnaireLine = JsonConvert.DeserializeObject<List<ApplicationQuestionnaireLineDTO>>(Obj_Response.Data.ToString());
                obj_ApplicationQuestionnaireLine = JsonConvert.DeserializeObject<List<ApplicationQuestionnaireLineDTO>>(Obj_Response.Data.ToString());

                //if (obj_ApplicationQuestionnaireLine.Count > 0)
                //{
                //    var Data = obj_ApplicationQuestionnaireLine.Find(x => x.Type == 1).Answer;
                //    if (Data != null && Data != "")
                //    {
                //        Question_NumberId = Convert.ToInt64(Data);
                //    }
                //}
            }
        }

        public List<ApplicationQuestionnaireLineAnswerDTO> GetAppsQuestionnariesAsync(short ntype)
        {
            return obj_ApplicationQuestionnaireLine.Find(x => x.Type == ntype).ApplicationQuestionnaireLineAnswer;
        }

        //Get Job Details
        public async Task GetAppQuestionnaireByNumberAsync()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetAppQuestionnaireByNumberAsync(ApplicationNumber);
            if (Obj_Response.IsSuccess)
            {
                AppsQuesModel = JsonConvert.DeserializeObject<ApplicationQuestionnaireLineDTO>(Obj_Response.Data.ToString());
            }
        }

        //public List<ApplicationQuestionnaireLineAnswerDTO> GetAppQuestionnaireAnswer(short type)
        //{
        //        List<ApplicationQuestionnaireLineAnswerDTO> enumerable = AppsQuesMainModel.ApplicationQuestionnaireLine.Where(x => x.Type == type).Select(y => y.ApplicationQuestionnaireLineAnswer); 
        //    return data.ToList();
        //}

        #region Add Edit Delete
        public async Task EditAppsQuestionnariesAsync()
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure, you want to submit?")) //for '{obj_JobDTO.Code}' job
                return;

            isValid = true;
            List<ApplicationQuestionnaireLineAnswerDTO> ApplicationQuestionnaire = new List<ApplicationQuestionnaireLineAnswerDTO>();

            // 0 = String; // 1 = Number; // 2 = Float; // 3 = Date; // 4 = Time; // 5 = Multiline  // 6 = CheckBox // 7 = Radio // 8 = DropDown

            IsLoaderShow = true;

            AppsQuesMainModel.ERPRecId = AppsQuesMainModel.ApplicationQuestionnaireLine[0].ApplicationQuestionnaireErpRecId;
            AppsQuesMainModel.StartDate = StartDateTime;
            AppsQuesMainModel.EndDate = DateTime.Now;

            foreach (var item in AppsQuesMainModel.ApplicationQuestionnaireLine)
            {
                if (item.Type != 1 && item.Type != 2 && item.Type != 3 && item.Type != 4 ) //Not Number && Not Float && Not Date && Not Time
                {
                    if (item.Answer == "" || item.Answer == null)
                    {
                        isValid = false;
                        obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                        IsLoaderShow = false;
                        return;
                    }
                    if (item.Type == 7) //Radio Button
                    {
                        if (item.Answer != "" || item.Answer != null)
                        {
                            ApplicationQuestionnaire = obj_ApplicationQuestionnaireLine.Find(x => x.Type == 7 && x.ERPRecId == item.ERPRecId).ApplicationQuestionnaireLineAnswer.ToList();
                            if (ApplicationQuestionnaire != null && ApplicationQuestionnaire.Count() > 0)
                            {
                                var data = ApplicationQuestionnaire.Where(x => x.Text.ToLower() == item.Answer.ToLower()).Select(y => y).FirstOrDefault();
                                item.AnswerErpRecId = Convert.ToInt64(data.ERPRecId);
                            }
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }

                    else if (item.Type == 8) //Drop Down
                    {
                        if (item.Answer != "" || item.Answer != null)
                        {
                            ApplicationQuestionnaire = obj_ApplicationQuestionnaireLine.Find(x => x.Type == 8 && x.ERPRecId == item.ERPRecId).ApplicationQuestionnaireLineAnswer.ToList();
                            if (ApplicationQuestionnaire != null && ApplicationQuestionnaire.Count() > 0)
                            {
                                long? Id = ApplicationQuestionnaire.Where(x => x.Text == item.Answer).Select(y => y).FirstOrDefault().ERPRecId;
                                item.AnswerErpRecId = Convert.ToInt64(Id);
                            }
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                    else if (item.Type == 6) //Check Box (Using Drop Down)
                    {
                        if (item.Answer != "" || item.Answer != null)
                        {
                            ApplicationQuestionnaire = obj_ApplicationQuestionnaireLine.Find(x => x.Type == 6 && x.ERPRecId == item.ERPRecId).ApplicationQuestionnaireLineAnswer.ToList();
                            if (ApplicationQuestionnaire != null && ApplicationQuestionnaire.Count() > 0)
                            {
                                long? Id = ApplicationQuestionnaire.Where(x => x.Text == item.Answer).Select(y => y).FirstOrDefault().ERPRecId;
                                item.AnswerErpRecId = Convert.ToInt64(Id);
                            }
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (item.Type == 1) //number
                    {
                        if (item.AnswerNumberId != null)
                        {
                            item.Answer = item.AnswerNumberId.ToString();
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                    if (item.Type == 2) //Float
                    {
                        if (item.AnswerFloatId != null)
                        {
                            item.Answer = item.AnswerFloatId.ToString();
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                    else if (item.Type == 3) //Date
                    {
                        if (item.AnswerDateTimeId.ToString() != "" || item.AnswerDateTimeId.ToString() != null)
                        {
                            item.Answer = item.AnswerDateTimeId.ToString();
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                    else if (item.Type == 4) //Time
                    {
                        if (item.AnswerTimeId.ToString() != "" || item.AnswerTimeId.ToString() != null)
                        {
                            item.Answer = item.AnswerTimeId.ToString();
                        }
                        else
                        {
                            isValid = false;
                            obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
                            IsLoaderShow = false;
                            return;
                        }
                    }
                    //else if (item.Type == 6) //Check Box
                    //{

                    //    //if (Question_CheckId)
                    //    //{
                    //    //    item.Answer = "Yes";
                    //    //    ///item.AnswerErpRecId = 1;
                    //    //}
                    //    //else
                    //    //{
                    //    //    item.Answer = "No";
                    //    //    //item.AnswerErpRecId = 0; 
                    //    //}

                    //    //item.Answer = item.Answer;
                    //    item.AnswerErpRecId = 0; 

                    //    //ApplicationQuestionnaire = obj_ApplicationQuestionnaireLine.Find(x => x.Type == 6 && x.ERPRecId == item.ERPRecId).ApplicationQuestionnaireLineAnswer.ToList();
                    //    //if (ApplicationQuestionnaire != null && ApplicationQuestionnaire.Count() > 0)
                    //    //{
                    //    //    long? Id = ApplicationQuestionnaire.Where(x => x.Text == item.Answer).Select(y => y).FirstOrDefault().ERPRecId;
                    //    //    item.AnswerErpRecId = Convert.ToInt64(Id);
                    //    //}
                    //}
                }
            }
            if (isValid)
            {
                Obj_Response = await _objRepo.EditQuestionnaireAsync(AppsQuesMainModel);
                if (Obj_Response.Status == "Success")
                {
                    InitializationForm();
                    GetMassage();
                    _nav.NavigateTo("appliedjob");
                }
                await GetAppsQuestionnariesAsync();
            }
            else
            {
                obj_snackbar.Add("Please answer all questions before submitting the questionnaire.", Severity.Error);
            }
            IsLoding = false;
        }
        #endregion

        public void GetMassage()
        {
            if (Obj_Response.Status == "Success")
                obj_snackbar.Add(Obj_Response.Message, Severity.Success);
            else if (Obj_Response.Status == "Warning")
                obj_snackbar.Add(Obj_Response.Message, Severity.Warning);
            else
                obj_snackbar.Add(Obj_Response.Message, Severity.Error);
        }

        public class CustomStringToBoolConverter : BoolConverter<string>
        {
            public CustomStringToBoolConverter()
            {
                SetFunc = OnSet;
                GetFunc = OnGet;
            }

            public const string TrueString = "Yes";
            public const string FalseString = "No";

            private string OnGet(bool? value) => value == true ? TrueString : FalseString;

            private bool? OnSet(string arg)
            {
                try
                {
                    if (arg == TrueString)
                        return true;
                    if (arg == FalseString)
                        return false;
                    return null;
                }
                catch (FormatException e)
                {
                    UpdateSetError("Conversion error: " + e.Message);
                    return null;
                }
            }
        }

        public async Task SeActiveTime(string ActiveStep)
        {
            if (IsStart)
            {
                StartDateTime = DateTime.Now;
                IsStart = false;
            }
            else
            { 
                IsStart = false;
            }
            StateHasChanged();
        }

        public async void signOut()
        {
            IsLoding = false;
            await SignOutManager.SetSignOutState();
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }
    }
}

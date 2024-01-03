using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandidatePortal.Client.Pages
{
    public partial class SignIn : ComponentBase
    {
        SignInDTO model = new SignInDTO();
        public string Password { get; set; } = "superstrong123";

        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        void ButtonTestclick()
        {
            if(isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        public async Task OnValidSubmit()
        {

        }


        //UserLoginModel userloginmodel { get; set; } = new UserLoginModel();
        public bool Basic_Switch2 { get; set; } = true;

        protected async Task UserLoginEvent()
        {

        }
    }
}

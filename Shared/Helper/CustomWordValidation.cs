using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Helper
{
    public class CustomWordValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not null && value.ToString() != "")
            {
                value = value.ToString().ToLower();
                if (value.ToString().Contains("select") || value.ToString().Contains("delete")
                    || value.ToString().Contains("insert") || value.ToString().Contains("update")
                    || value.ToString().Contains("read") || value.ToString().Contains("from")
                    || value.ToString().Contains("table"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}

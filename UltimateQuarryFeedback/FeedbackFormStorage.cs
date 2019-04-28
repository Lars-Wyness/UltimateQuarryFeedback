using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UltimateQuarryFeedback
{
    public class FeedbackFormStorage
    {
        public List<FeedbackForm> feedbackForms = new List<FeedbackForm>();

        public void addFeedbackForm(FeedbackForm form)
        { 
            form.Validate(feedbackForms);
            feedbackForms.Add(form);
        }
    }
}

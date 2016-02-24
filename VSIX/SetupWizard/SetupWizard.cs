﻿using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace Christoc.DNNTemplates.SetupWizard
{
    public class SetupWizard : IWizard
    {
        private DTE _dte;
        private bool _shouldAddProjectItem;

        public void BeforeOpeningFile(ProjectItem projectItem) { }
        public void ProjectFinishedGenerating(Project project) { }
        public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
        public void RunFinished() { }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = (DTE)automationObject;


            _shouldAddProjectItem = false;
            var inputForm = new WizardView();
            inputForm.ShowDialog();
            if (inputForm.OwnerEmail == null || inputForm.RootNameSpace == null || inputForm.OwnerName == null || inputForm.OwnerWebsite == null || inputForm.DevEnvironmentUrl == null) return;
            _shouldAddProjectItem = true;
            replacementsDictionary.Remove("$rootnamespace$");
            replacementsDictionary.Remove("$ownername$");
            replacementsDictionary.Remove("$owneremail$");
            replacementsDictionary.Remove("$ownerwebsite$");
            replacementsDictionary.Remove("$devenvironmenturl$");

            replacementsDictionary.Add("$rootnamespace$", inputForm.RootNameSpace);
            replacementsDictionary.Add("$ownername$", inputForm.OwnerName);
            replacementsDictionary.Add("$owneremail$", inputForm.OwnerEmail);
            replacementsDictionary.Add("$ownerwebsite$", inputForm.OwnerWebsite);
            replacementsDictionary.Add("$devenvironmenturl$", inputForm.DevEnvironmentUrl);

            //// add an entry to the dictionary to specify the string used for the $rootnamespace$ token 
            //replacementsDictionary.Add("$rootnamespace2$", "Christoc.Dnn.Modules");
            //replacementsDictionary.Add("$ownername2$", "Christoc.com");
            //replacementsDictionary.Add("$owneremail2$", "modules@christoc.com");
            //replacementsDictionary.Add("$ownerwebsite2$", "http://www.christoc.com");
            //replacementsDictionary.Add("$devenvironmenturl2$", "dnndev.me");
        }

        public bool ShouldAddProjectItem(string filePath) { return true; }
    }
}
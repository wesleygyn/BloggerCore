using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BloggerCore.Controllers
{
    public abstract class EditorFileBrowserController : BaseFileBrowserController
    {
        /// <summary>
        /// Gets the valid file extensions by which served files will be filtered.
        /// </summary>
        public override string Filter
        {
            get
            {
                return EditorFileBrowserSettings.DefaultFileTypes;
            }
        }

        public EditorFileBrowserController(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {

        }
    }
}

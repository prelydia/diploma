#pragma checksum "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4f0ae069bb40a66d015251607459f444c21d97a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_FoldersFiles), @"mvc.1.0.view", @"/Views/User/FoldersFiles.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\_ViewImports.cshtml"
using diploma;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\_ViewImports.cshtml"
using diploma.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
using Microsoft.AspNetCore.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4f0ae069bb40a66d015251607459f444c21d97a", @"/Views/User/FoldersFiles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd751bc93a5f3e50fa5254fce3eb57576acfa3b9", @"/Views/_ViewImports.cshtml")]
    public class Views_User_FoldersFiles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<diploma.Models.File>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/folders_prev.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 60px; height: 60px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-bottom: 10px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/file_icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 30px; height: 30px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddFile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 12 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
   ViewBag.Title = ViewBag.FoldName; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>

    .files {
        float: left;
    }

    .item {
        float: left;
        width: stretch;
    }

    .c-button {
        border: 2px solid transparent;
        /* другие стили */
        background: transparent;
        /*color: #ffffff;*/
        border-color: #000000;
        height: 40px;
        width: 128px;
    }
</style>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4f0ae069bb40a66d015251607459f444c21d97a8340", async() => {
                WriteLiteral("\r\n    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js\"></script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n    <div class=\"files\" style=\"margin-bottom: 10px;\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b4f0ae069bb40a66d015251607459f444c21d97a9505", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n    <div class=\"files\">\r\n        <p style=\"margin-left: 10px; margin-top: 4px; font-size: 18px;\"> <b> ");
#nullable restore
#line 45 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                        Write(ViewBag.FoldName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </b> </p>\r\n        <p class=\"text-muted\" style=\"margin-top: -15px; margin-left: 10px; font-size: 14px;\"> ");
#nullable restore
#line 46 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                         Write(Localizer["Content"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 46 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                                               Write(ViewBag.FoldName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </p>
    </div>

    <div class=""files"" style=""float: right;"">
        <button class=""c-button"" data-toggle=""modal"" data-target=""#addfile"" style=""border-radius:30px; margin-left: 15px; margin-bottom: 10px; margin-top: 10px; margin-right: 15px; width: 172px;""> ");
#nullable restore
#line 50 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                                                                                                                                Write(Localizer["Add"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </button>
    </div>

    <div class=""files"" style=""float: right;"">
        <input type=""image"" onclick=""window.location.href='/User/ShowFolders'"" title=""Список папок"" src=""/images/left-icon.png"" style=""width: 20px; height: 20px; margin-top: 20px;"">
    </div>

    <p>⠀⠀⠀⠀⠀⠀⠀⠀⠀</p>
    <p>⠀⠀⠀⠀⠀⠀⠀⠀⠀</p>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4f0ae069bb40a66d015251607459f444c21d97a12818", async() => {
                WriteLiteral("\r\n\r\n    <div class=\"container\">\r\n\r\n        <div class=\"item\">\r\n            <input name=\"name\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2130, "\"", 2162, 1);
#nullable restore
#line 66 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 2144, Localizer["Name"], 2144, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n        </div>\r\n\r\n        <div class=\"item\">\r\n            <label class=\"control-label\">  </label>\r\n        </div>\r\n\r\n        <div class=\"item\">\r\n            <input name=\"contains\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2368, "\"", 2402, 1);
#nullable restore
#line 74 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 2382, Localizer["Search"], 2382, 20, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" style=""width: 450px;"" class=""form-control"" />
        </div>

        <div class=""item"">
            <input name=""seldate"" type=""date"" min=""2018-02-22"" id=""date"" class=""form-control"">
        </div>

        <div class=""item"">
            <input type=""submit""");
                BeginWriteAttribute("value", " value=\"", 2671, "\"", 2699, 1);
#nullable restore
#line 82 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 2679, Localizer["Filter"], 2679, 20, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" />\r\n        </div>\r\n\r\n    </div>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<p>⠀⠀⠀⠀⠀⠀⠀⠀⠀</p>\r\n\r\n\r\n");
#nullable restore
#line 92 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
 if (Model.Any() == false)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<p class=\"text-muted\" style=\"margin-left: 15px; margin-top: 20px; font-size: 26px;\"> <b> ");
#nullable restore
#line 94 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                    Write(Localizer["Empty"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </b> </p>");
#nullable restore
#line 94 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                                                      }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 96 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
 foreach (var file in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container\" style=\"display: flex;\">\r\n    <a class=\"list-group-item list-group-item-action\">\r\n        <div class=\"d-flex w-100 justify-content-between\">\r\n            <p class=\"mb-1\"> ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "b4f0ae069bb40a66d015251607459f444c21d97a17632", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" ");
#nullable restore
#line 101 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                              Write(file.Name.Substring(0, file.Name.LastIndexOf("_")));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </p>
        </div>
    </a>

    <div class=""testcont"" style=""float: right; display: flex;"">
        <div class=""container"" style=""display: flex; margin-right: -28px;"">
            <a class=""list-group-item list-group-item-action"" style=""width: 60px;"">
                <div class=""item one"">
                    <input type=""image"" src=""/images/edit-icon.png"" data-toggle=""modal"" data-target=""#rename""");
            BeginWriteAttribute("pathId", " pathId=\"", 3703, "\"", 3720, 1);
#nullable restore
#line 109 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 3712, file.Id, 3712, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("pathDefault", " pathDefault=\"", 3721, "\"", 3745, 1);
#nullable restore
#line 109 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 3735, file.Path, 3735, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("title", "\r\n                           title=\"", 3746, "\"", 3802, 1);
#nullable restore
#line 110 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 3782, Localizer["Rename"], 3782, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""width: 25px; height: 25px; position: relative;"" onclick=""getAttrb()"" />
                </div>
            </a>
        </div>

        <div class=""container"" style=""display: flex; margin-right: -28px;"">
            <a class=""list-group-item list-group-item-action"" style=""width: 60px;"" href=""#"">
                <div class=""item one"">
                    <input type=""image"" src=""/images/eye.png"" formtarget=""_blank""");
            BeginWriteAttribute("onClick", " onClick=\"", 4236, "\"", 4348, 4);
            WriteAttributeValue("", 4246, "window.open(\'", 4246, 13, true);
#nullable restore
#line 118 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 4259, Url.Action("View", "User", new { fileToView = file.Name, pathh = file.Path}), 4259, 77, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4336, "\',", 4336, 2, true);
            WriteAttributeValue(" ", 4338, "\'_blank\')", 4339, 10, true);
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 4349, "\"", 4376, 1);
#nullable restore
#line 118 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 4357, Localizer["Watch"], 4357, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""width: 25px; height: 25px; position: relative;"" />
                </div>
            </a>
        </div>

        <div class=""container"" style=""display: flex; margin-right: -28px;"">
            <a class=""list-group-item list-group-item-action"" style=""width: 60px;"">
                <div class=""item one"">
                    <input type=""image"" src=""/images/download.png""");
            BeginWriteAttribute("onclick", " onclick=\"", 4765, "\"", 4873, 3);
            WriteAttributeValue("", 4775, "location.href=\'", 4775, 15, true);
#nullable restore
#line 126 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 4790, Url.Action("Download", "File", new { fileToView = file.Name, pathh = file.Path }), 4790, 82, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4872, "\'", 4872, 1, true);
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 4874, "\"", 4904, 1);
#nullable restore
#line 126 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 4882, Localizer["Download"], 4882, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""width: 25px; height: 25px; position: relative;"" />
                </div>
            </a>
        </div>

        <div class=""container"" style=""display: flex;"">
            <a class=""list-group-item list-group-item-action"" style=""width: 60px;"">
                <div class=""item one"">
                    <input type=""image"" id=""deleteButton"" src=""/images/delete.png"" onclick=""deleteConfirm(event)""");
            BeginWriteAttribute("fileid", " fileid=\"", 5319, "\"", 5336, 1);
#nullable restore
#line 134 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 5328, file.Id, 5328, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("path", " path=\"", 5337, "\"", 5354, 1);
#nullable restore
#line 134 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 5344, file.Path, 5344, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 5355, "\"", 5383, 1);
#nullable restore
#line 134 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
WriteAttributeValue("", 5363, Localizer["Delete"], 5363, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width: 25px; height: 25px; position: relative;\" />\r\n                </div>\r\n            </a>\r\n        </div>\r\n    </div>\r\n</div>");
#nullable restore
#line 139 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
      }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- Модальное окно редактирования файла -->
<div id=""rename"" class=""modal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title""> ");
#nullable restore
#line 146 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                    Write(Localizer["Rename"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""form-group"">
                    <label>");
#nullable restore
#line 153 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                      Write(Localizer["Name"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                    <input type=""text"" class=""form-control"" id=""newName"" />
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""submit"" class=""btn btn-primary"" onclick=""renameMethod()""> ");
#nullable restore
#line 158 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                   Write(Localizer["Save"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </button>\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\"> ");
#nullable restore
#line 159 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                 Write(Localizer["Close"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </button>
            </div>
        </div>
    </div>
</div>


<script type=""module"" src=""https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js""></script>
<script nomodule src=""https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js""></script>

<!-- Модальное окно добавления файла в систему -->
<div id=""addfile"" class=""modal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title""> ");
#nullable restore
#line 174 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                    Write(Localizer["Add"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4f0ae069bb40a66d015251607459f444c21d97a28513", async() => {
                WriteLiteral(@"

                    <div class=""input-group mb-3"">
                        <div class=""input-group-prepend"">
                            <button type=""submit"" class=""btn btn-outline-secondary"">
                                <span class=""button-text""> ");
#nullable restore
#line 185 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                      Write(Localizer["Download"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" </span>
                                <span class=""button-icon"">
                                    <ion-icon name=""cloud-download-outline""></ion-icon>
                                </span>
                            </button>
                        </div>
                        <div class=""custom-file"">
                            <input type=""file"" class=""custom-file-input"" name=""uploadedFile"">
                            <label class=""custom-file-label"" for=""customFile""> ");
#nullable restore
#line 193 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                          Write(Localizer["Choose"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </label>\r\n                        </div>\r\n                    </div>\r\n\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\"> ");
#nullable restore
#line 200 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                                                                                 Write(Localizer["Close"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </button>
            </div>
        </div>
    </div>
</div>


<script>

    var id; // id файла из БД (метод переименования)
    var path; // путь файла из БД (метод переименования)

    // Add the following code if you want the name of the file appear on select
    $("".custom-file-input"").on(""change"", function () {
        var fileName = $(this).val().split(""\\"").pop();
        $(this).siblings("".custom-file-label"").addClass(""selected"").html(fileName);
    });

    // helper для EditFile
    function getAttrb(e) {

        id = event.target.getAttribute(""pathId"");
        path = event.target.getAttribute(""pathDefault"");

    }

    // редактирование файла
    function renameMethod(e) {


        $.ajax({
            url: '/User/EditFile',
            type: 'POST',
            contentType: false,
            processData: false,
            data: JSON.stringify(id + ""+"" + path + ""+"" + $(""#newName"").val()),
            contentType: 'application/json; charset=utf-8',
     ");
            WriteLiteral("       success: function (data) {\r\n\r\n                location.reload();\r\n\r\n\r\n            },\r\n            error: function () {\r\n                Swal.fire({\r\n                    icon: \"error\",\r\n                    title: \"");
#nullable restore
#line 246 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                       Write(Localizer["Wrong"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    text: \"");
#nullable restore
#line 247 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                      Write(Localizer["Verify"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                });
            }
        });

    }

    function deleteConfirm(event) {

        var fileId = event.target.getAttribute('fileid');
        var filePath = event.target.getAttribute('path');

        Swal.fire({
            title: """);
#nullable restore
#line 260 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
               Write(Localizer["Sure"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n            text: \"");
#nullable restore
#line 261 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
              Write(Localizer["ConfirmDelete"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n            icon: \"warning\",\r\n            showCancelButton: true,\r\n            confirmButtonText: \"");
#nullable restore
#line 264 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                           Write(Localizer["Yes"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n            confirmButtonColor: \"#1e90ff\",\r\n            cancelButtonText: \"");
#nullable restore
#line 266 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\User\FoldersFiles.cshtml"
                          Write(Localizer["Cancel"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
        }).then(function (result) {

            if (result.isConfirmed) {
                var url = '/User/DeleteConfirmed';
                url = url + '?id=' + fileId + '&pathh=' + filePath;
               
                // Отправляем AJAX запрос на удаление файла
                $.ajax({
                    url: url,
                    type: 'GET',
                    success: function (response) {
                        // Если удаление прошло успешно, обновляем страницу
                        window.location.reload();
                    },
                    error: function (xhr, status, error) {
                        // Обработка ошибок при удалении файла
                        console.error(error);
                    }
                });
            }

        });

    }

</script>






");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IOptions<RequestLocalizationOptions> LocOptions { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<diploma.Models.File>> Html { get; private set; }
    }
}
#pragma warning restore 1591
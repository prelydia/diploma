#pragma checksum "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43c130e5886b128bee5eeb204be1be3b947729ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Role_Index), @"mvc.1.0.view", @"/Views/Role/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43c130e5886b128bee5eeb204be1be3b947729ea", @"/Views/Role/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd751bc93a5f3e50fa5254fce3eb57576acfa3b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Role_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<diploma.Models.Role>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/role.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 70px; height: 70px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("validation"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
  
    ViewBag.Title = "Список ролей";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43c130e5886b128bee5eeb204be1be3b947729ea4658", async() => {
                WriteLiteral("\r\n    <script src=\"jquery-3.6.3.min.js\"></script>\r\n");
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
            WriteLiteral(@"

<style>
    .files {
        float: left;
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

<!-- Картинка с папкой -->
<div class=""container"">
    <div class=""files"" style=""margin-bottom: 10px;"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "43c130e5886b128bee5eeb204be1be3b947729ea6098", async() => {
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
            WriteLiteral(@"
    </div>

    <div class=""files"">
        <p style=""margin-left: 10px; margin-top: 4px; font-size: 18px;""> <b> Роли </b> </p>
        <p class=""text-muted"" style=""margin-top: -15px; margin-left: 10px; font-size: 14px;""> Все роли, реализованные в системе </p>
    </div>

    <div class=""files"" style=""float: right;"">
        <button class=""c-button"" data-toggle=""modal"" data-target=""#addrole"" style=""border-radius:30px; margin-left: 15px; margin-bottom: 10px; margin-top: 10px; margin-right: 15px; width: 172px;""> Добавить </button>
    </div>

    <p>⠀⠀⠀⠀⠀⠀⠀⠀⠀</p>
    <p>⠀⠀⠀⠀⠀⠀⠀⠀⠀</p>

</div>

<!-- В случае отсутствия файлов -->
");
#nullable restore
#line 47 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
 foreach (Role role in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container\" style=\"display: flex;\">\r\n        <a class=\"list-group-item list-group-item-action\">\r\n            <div class=\"d-flex w-100 justify-content-between\">\r\n                <p class=\"mb-1\"> ");
#nullable restore
#line 52 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
                            Write(role.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n            </div>\r\n        </a>\r\n\r\n");
            WriteLiteral(@"
        <div class=""testcont"" style=""float: right; display: flex;"">
            <div class=""container"" style=""display: flex; margin-right: -28px;"">
                <a class=""list-group-item list-group-item-action"" style=""width: 60px;"">
                    <div class=""item one"">
                        <input type=""image"" src=""/images/edit-icon.png""
                               title=""Изменить"" style=""width: 25px; height: 25px; position: relative;""");
            BeginWriteAttribute("name", " name=\"", 2341, "\"", 2356, 1);
#nullable restore
#line 67 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
WriteAttributeValue("", 2348, role.Id, 2348, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" onclick=""getId()"" data-toggle=""modal"" data-target=""#editrole"" />
                    </div>
                </a>
            </div>

            <div class=""container"" style=""display: flex;"">
                <a class=""list-group-item list-group-item-action"" style=""width: 60px;"">
                    <div class=""item one"">
                        <input type=""image"" src=""/images/delete.png"" onclick=""deleteRole()""");
            BeginWriteAttribute("name", " name=\"", 2780, "\"", 2795, 1);
#nullable restore
#line 75 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
WriteAttributeValue("", 2787, role.Id, 2787, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Удалить\" style=\"width: 25px; height: 25px; position: relative;\" />\r\n                    </div>\r\n                </a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 81 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script type=""module"" src=""https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js""></script>
<script nomodule src=""https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js""></script>

<!-- Модальное окно добавления пользователя в систему (добавить проверку по полям) -->
<div id=""addrole"" class=""modal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title""> Добавить роль  </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43c130e5886b128bee5eeb204be1be3b947729ea11512", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 98 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                <div class=""form-group"">
                    <label> Название </label><br />
                    <input type=""text"" id=""name"" class=""form-control"" />
                </div>

            </div>
            <div class=""modal-footer"">
                <input type=""submit"" id=""create"" onclick=""addRole()"" value=""Создать"" class=""btn btn-primary"" />
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal""> Закрыть </button>
            </div>
        </div>
    </div>
</div>

<!-- Модальное окно изменения выбранной роли  -->
<div id=""editrole"" class=""modal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title""> Изменить роль  </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
          ");
            WriteLiteral("      </button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43c130e5886b128bee5eeb204be1be3b947729ea14320", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 125 "C:\Users\l.khusainova\source\repos\diploma\diploma\Views\Role\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                <div class=""form-group"">
                    <label> Название </label><br />
                    <input type=""text"" id=""newName"" class=""form-control"" />
                </div>

            </div>
            <div class=""modal-footer"">
                <input type=""submit"" id=""create"" onclick=""editRole()"" value=""Сохранить"" class=""btn btn-primary"" />
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal""> Закрыть </button>
            </div>
        </div>
    </div>
</div>


<script type=""text/javascript"">

    var newId;

    // helper
    function getId() {

        newId = event.target.getAttribute(""name"");
    }

    function editRole() {


        $.ajax({
            url: '/Role/Edit',
            type: 'POST',
            contentType: false,
            processData: false,
            data: JSON.stringify(newId + ""+"" + $(""#newName"").val()),
            contentType: 'application/json; charset=utf-8',
            success: ");
            WriteLiteral(@"function (data) {

                setTimeout(location.reload(), 500);

            },
            error: function () {
                Swal.fire({
                    icon: ""error"",
                    title: ""Что-то не так..."",
                    text: ""Проверьте правильность введенных данных""
                });
            }
        });
    }

    function deleteRole() {

        var id = event.target.getAttribute(""name"");

        Swal.fire({
            title: ""Вы уверены?"",
            text: ""Вы уверены, что хотите удалить эту роль?"",
            icon: ""warning"",
            showCancelButton: true,
            confirmButtonText: ""Да"",
            confirmButtonColor: ""#1e90ff"",
            cancelButtonText: ""Отмена"",
        }).then(function (result) {

            $.ajax({
                url: '/Role/Delete',
                type: 'POST',
                contentType: false,
                processData: false,
                data: JSON.stringify(id),
                ");
            WriteLiteral(@"contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    setTimeout(location.reload(), 500);

                },
                error: function () {
                    Swal.fire({
                        icon: ""error"",
                        title: ""Что-то не так..."",
                        text: ""Проверьте правильность введенных данных""
                    });
                }
            });

        });

    }

    function addRole() {
        var name = $(""#name"").val();

        if (name == """") {
            Swal.fire({
                icon: ""error"",
                title: ""Что-то не так..."",
                text: ""Введите название роли!""
            });
        }
        else {

            $.ajax({
                url: '/Role/Create',
                type: 'POST',
                contentType: false,
                processData: false,
                data: JSON.stringify(name),
                contentType: ");
            WriteLiteral(@"'application/json; charset=utf-8',
                success: function (data) {

                    location.reload();


                },
                error: function (data) {

                    Swal.fire({
                        icon: ""error"",
                        title: ""Что-то не так..."",
                        text: ""Проверьте правильность введенных данных""
                    });

                }
            });

        }
    }
</script>

");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<diploma.Models.Role>> Html { get; private set; }
    }
}
#pragma warning restore 1591

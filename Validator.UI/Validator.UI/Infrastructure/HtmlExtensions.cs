namespace Validator.UI
{
    #region << Using >>

    using System;
    using System.Web.Mvc;
    using Incoding.MvcContrib;

    #endregion

    public static class HtmlExtensions
    {
        public static IDisposable BeginPush<T>(this HtmlHelper<T> htmlHelper, Action<Setting> evaluated)
        {
            var setting = new Setting();
            evaluated(setting);

            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return htmlHelper.When(JqueryBind.Submit)
                             .PreventDefault()
                             .Submit()
                             .OnBegin(dsl => dsl.Self().Core().Form.Validation.Parse())
                             .OnSuccess(dsl =>
                                        {
                                            if (setting.OnSuccess != null)
                                                setting.OnSuccess(dsl);
                                        })
                             .OnError(dsl => dsl.Self().Core().Form.Validation.Refresh())
                             .AsHtmlAttributes()
                             .ToBeginForm(htmlHelper, url.Dispatcher().Push(new TestCommand()));
        }

        public class Setting
        {
            public Action<IIncodingMetaLanguageCallbackBodyDsl> OnSuccess { get; set; }
        }
    }
}
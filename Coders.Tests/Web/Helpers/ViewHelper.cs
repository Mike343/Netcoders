using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace Coders.Tests.Web.Helpers
{
	public class ViewHelper
	{
		public ViewHelper() 
			: this(string.Empty)
		{

		}

		public ViewHelper(string name)
		{
			RouteTable.Routes.Clear();
			RouteTable.Routes.MapRoute("test", "test/fire", new { controller = "test", action = "fire" });

			var http = MockRepository.GenerateMock<HttpContextBase>();
			var request = MockRepository.GenerateMock<HttpRequestBase>();
			var response = MockRepository.GenerateMock<HttpResponseBase>();
			var controller = MockRepository.GenerateMock<ControllerBase>();

			request.Stub(x => x.ApplicationPath).Return("/");

			http.Stub(x => x.Request).Return(request);

			response.Stub(x => x.Output).Return(new StringWriter());

			http.Stub(x => x.Response).Return(response);

			var route = new RouteData();

			route.Values.Add("controller", "test");
			route.Values.Add("action", "fire");

			var context = new ControllerContext(new RequestContext(http, route), controller);
			var engine = MockRepository.GenerateMock<IViewEngine>();
			var view = MockRepository.GenerateMock<IView>();
			var collection = MockRepository.GenerateMock<ViewEngineCollection>();

			collection.Stub(x => x.FindView(context, name, string.Empty)).Return(new ViewEngineResult(view, engine));

			var viewData = new ViewDataDictionary();
			var viewDataContainer = MockRepository.GenerateStub<IViewDataContainer>();

			viewDataContainer.ViewData = viewData;

			this.ControllerContext = context;
			this.ViewEngineCollection = collection;
			this.ViewContext = new ViewContext(context, view, new ViewDataDictionary(), new TempDataDictionary(), new StringWriter());
			this.HtmlHelper = new HtmlHelper(this.ViewContext, viewDataContainer);
			this.UrlHelper = new UrlHelper(context.RequestContext);
		}

		public ControllerContext ControllerContext
		{
			get;
			private set;
		}

		public ViewContext ViewContext
		{
			get; 
			private set;
		}

		public ViewEngineCollection ViewEngineCollection
		{
			get; 
			private set;
		}

		public HtmlHelper HtmlHelper
		{
			get; 
			private set;
		}

		public UrlHelper UrlHelper
		{
			get;
			private set;
		}
	}
}
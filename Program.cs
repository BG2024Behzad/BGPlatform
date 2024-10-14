var builder = WebApplication.CreateBuilder(args); //  - responsible for setting up and configuring the fundamental components
                                                  //  - run HTTP web server named 'Kestral' (Kestral: to handle incoming HTTP)
var app = builder.Build(); // finalize the configuration of the application and create an instance of the WebApplication class

// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
// making branch
((IApplicationBuilder)app).Map("/branch", branch =>
{
    branch.UseMiddleware<Platform.QueryStringMiddleWare>();
    /*branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });*/
    branch.Run(async (context) =>  // app.Run() is a terminal middleware because it handles the request and does not pass
                                   // control to any further middleware using next(). This means it ends the request pipeline here.
    {
        await context.Response.WriteAsync($"terminal middleware");
    });
});

// end making branch
// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

/*app.Use(async (context, next) =>
{
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/short")
    {
        await context.Response.WriteAsync($"Request Short Circuited");
    } else {
        await next();
    }
});

// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
// making custom middlware
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middlware \n");
    }
    await next();
});*/
//HttpContext represents all the information about an HTTP request and response for a specific client-server interaction.
//It provides access to various features and data related to the current request,
//such as user information, request headers, response headers, session data, and much more.
// end making custom middlware
// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

app.UseMiddleware<Platform.QueryStringMiddleWare>();

app.MapGet("/", () => "Hello World!"); // - is used to define a route that handles HTTP GET requests in the request processing pipeline.
                                       // - "/": the root of the website [MapGet is an Extension Method]

app.Run(); // - start the request processing pipeline and begin listening for incoming HTTP requests (Kestral).
           // - also finalizes the setup of the middleware pipeline.

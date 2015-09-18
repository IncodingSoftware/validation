# command
    public class TestCommand : CommandBase
    {
        public string NotEmpty { get; set; }

        public string Must { get; set; }

        public string ThrowFromCommand { get; set; }

        public override void Execute()
        {
            throw IncWebException.For<TestCommand>(command => command.ThrowFromCommand, "Throw form command");
        }

        public class Validator : AbstractValidator<TestCommand>
        {
            public Validator()
            {
                RuleFor(r => r.NotEmpty).NotEmpty();
                RuleFor(r => r.Must).Must(s => s != "Test").WithMessage("Value it is 'Test'");
            }
        }
    }

# Submit form (cshtml)
@using (Html.When(JqueryBind.Submit)
            .PreventDefault()
            .Submit()
            .OnBegin(dsl => dsl.Self().Core().Form.Validation.Parse())
            .OnSuccess(dsl => dsl.Utilities.Window.Alert("Success"))
            .OnError(dsl => dsl.Self().Core().Form.Validation.Refresh())
            .AsHtmlAttributes()
            .ToBeginForm(Html, Url.Dispatcher().Push(new TestCommand())))
{
    @Html.ForGroup(r => r.NotEmpty).TextBox(control => { control.Label.Name = "Not Empty"; }) // work on client (also checking on server) because can translated by Fluent Validation
    @Html.ForGroup(r => r.Must).TextBox(control => { control.Label.Name = "Value should not be 'Test'"; }) // work ONLY server side
    @Html.ForGroup(r => r.ThrowFromCommand).TextBox(control => { control.Label.Name = "Throw form command"; }) // work ONLY server side
    <input type="submit" value="Submit"/>
}

# Submit form with html extensions
@using (Html.BeginPush(setting => { setting.OnSuccess = dsl => dsl.Utilities.Window.Alert("Success"); }))
{
    @Html.ForGroup(r => r.NotEmpty).TextBox(control => { control.Label.Name = "Not Empty"; }) 
    @Html.ForGroup(r => r.Must).TextBox(control => { control.Label.Name = "Value should not be 'Test'"; })
    @Html.ForGroup(r => r.ThrowFromCommand).TextBox(control => { control.Label.Name = "Throw form command"; })
    <input type="submit" value="Submit"/>
}

namespace Validator.UI
{
    using FluentValidation;
    using Incoding;
    using Incoding.CQRS;

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
}
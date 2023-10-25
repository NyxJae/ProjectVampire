namespace EGO.System
{
    public interface IQuectionContainer
    {
        Choice GetChoice(string choiceName);
    }

    public interface IQuestionContainer<T> :IQuectionContainer where T : IQuectionContainer
    {
        QuestionView<T> BeginQuestion();

    }
}
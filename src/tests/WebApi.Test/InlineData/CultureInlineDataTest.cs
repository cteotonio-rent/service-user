namespace WebApi.Test.InlineData
{
    public class CultureInlineDataTest: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "pt-BR" };
            yield return new object[] { "en-US" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    }
}

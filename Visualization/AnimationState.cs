
namespace Visualization;

public class AnimationState
{
    private List<IEnumerable<int>> _subStates = new();
    private IEnumerator<int>? _current;
    private int _currentIndex = 0;
    
    public void Add(Action<int> action)
    {
        _subStates.Add(_addInternal(action));
    }

    private static IEnumerable<int> _addInternal(Action<int> action)
    {
        for (var i = 0; i < 1000; i++)
        {
            action(i);
            yield return i;
        }
    }

    public void MoveNext()
    {
        if (_currentIndex >= _subStates.Count) return;
        
        _current ??= _subStates[_currentIndex].GetEnumerator();

        if (_current.MoveNext()) return;
        
        _currentIndex += 1;
        _current = null;
    }
}
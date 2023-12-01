
namespace Visualization;

public class AnimationState
{
    private List<int> _frames = new();
    private List<Action<int>> _actions = new();
    private IEnumerator<int>? _current;
    
    
    
    public AnimationState Add(Action<int> action)
    {
        _actions.Add(action);
        _frames.Add(600); 
        
        return this;
    }
    
    public AnimationState Frames(int frames)
    {
        _frames.RemoveAt(_frames.Count - 1);
        _frames.Add(frames);
        return this;
    }
    
    private IEnumerator<int>? _popAction()
    {
        if (_frames.Count == 0) return null;
        var frames = _frames.First(); 
        _frames.RemoveAt(0);
        
        if (_actions.Count == 0) return null;
        var action = _actions.First();
        _actions.RemoveAt(0);

        return _createEnumerator(frames, action);
    }

    private IEnumerator<int>? _createEnumerator(int frames, Action<int> action)
    {
        for (var i = 0; i < frames; i++)
        {
            action(i);
            yield return i;
        }
    }
    
    public void MoveNext()
    {
        //if (_currentIndex >= _subStates.Count) return;

        _current ??= _popAction();

        if (_current == null) return;
        if (_current.MoveNext()) return;
        
        _current = null;
    }
}
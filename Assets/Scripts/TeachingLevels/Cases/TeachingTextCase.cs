using Assets.Interface;
using TMPro;

namespace Assets.TeachingLevels.Cases
{
    public class TeachingTextCase : TeachingCase
    {
        private readonly TMP_Text _text;

        public TeachingTextCase(TMP_Text text, ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {
            _text = text;
        }

        public void WaitFillTextArea()
        {
            base.EnterWaitUntilCase(Temp);
        }
        
        private bool Temp()
        {
            if (_text)
            {
                //_text.    
            }
            
            return false;
        }
    }
}
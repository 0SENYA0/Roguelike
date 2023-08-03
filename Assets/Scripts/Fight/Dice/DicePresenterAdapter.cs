using System;

namespace Assets.Fight.Dice
{
    public class DicePresenterAdapter : IDisposable
    {
        private readonly DicePresenter _leftDice;
        private readonly DicePresenter _centerDice;
        private readonly DicePresenter _rightDice;

        public DicePresenterAdapter(DicePresenter leftDice, DicePresenter centerDice, DicePresenter rightDice)
        {
            _leftDice = leftDice;
            _centerDice = centerDice;
            _rightDice = rightDice;
        }

        public void SetDisactive()
        {
            
        }

        public void SetActive()
        {
            
        }

        public void Dispose()
        {
            _leftDice?.Dispose();
            _centerDice?.Dispose();
            _rightDice?.Dispose();
        }

        public bool CheckOnDicesShuffeled() =>
            _leftDice.WasShuffeled && _centerDice.WasShuffeled && _rightDice.WasShuffeled;
    }
}
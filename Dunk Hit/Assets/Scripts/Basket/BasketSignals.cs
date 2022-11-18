using Enums;
using Extentions;
using UnityEngine.Events;

namespace Basket
{
    public class BasketSignals : MonoSingleton<BasketSignals>
    {
        public UnityAction onChangeBasketDirection = delegate {  };
    }
}
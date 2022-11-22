using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel = delegate {  };
        public UnityAction<UIPanels> onClosePanel = delegate {  };
        public UnityAction onSetTimer = delegate {  };
        public UnityAction onCheckOverTime = delegate {  };
        public UnityAction<int> onSetScoreChange = delegate {  };
        public UnityAction<int> onSetHighScoreChange = delegate {  };
        public UnityAction<int> onSetGainScoreChange = delegate {  };
               

    }
}
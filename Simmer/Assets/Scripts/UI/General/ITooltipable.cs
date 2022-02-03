using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.UI
{
    public interface ITooltipable
    {
        public void SetParent(ITooltipable reference, bool isTarget);
    }
}
using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using Il2Cpp;

[assembly: MelonInfo(typeof(InstantScratch.Mod), "Instant Scratch", "1.0.0", "Yorupy")]
[assembly: MelonGame("Lunch Money Games", "Scritchy Scratchy")]

namespace InstantScratch
{
    public class Mod : MelonMod
    {
        public override void OnUpdate()
        {
            var kb = Keyboard.current;
            if (kb != null && kb.f7Key.wasPressedThisFrame) ScratchAll();
        }

        void ScratchAll()
        {
            int done = 0, skipped = 0;
            try
            {
                var tickets = UnityEngine.Object.FindObjectsOfType<Ticket>();
                foreach (var t in tickets)
                {
                    try
                    {
                        if (t == null || !t.gameObject.activeInHierarchy) { skipped++; continue; }
                        if (t.AllScratched) { skipped++; continue; }
                        t.Scratch();
                        done++;
                    }
                    catch (Exception e) { LoggerInstance.Warning("Scratch: " + e.Message); }
                }
            }
            catch (Exception e) { LoggerInstance.Error("ScratchAll: " + e); }
            LoggerInstance.Msg($"F7: стёрто билетов {done}, пропущено {skipped}");
        }
    }
}
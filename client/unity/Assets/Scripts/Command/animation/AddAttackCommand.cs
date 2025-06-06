using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCity
{
    public class AddAttackCommand : AbstractCommand
    {
        private TankModel player;

        public AddAttackCommand(TankModel tank)
        {
            player = tank;
        }

        protected override void OnExecute()
        {
            int t_IsAttacking = player.TankObject.GetComponent<Animator>().GetInteger("IsAttacking");
            player.TankObject.GetComponent<Animator>().SetInteger("IsAttacking", t_IsAttacking + 1);
        }
    }
}


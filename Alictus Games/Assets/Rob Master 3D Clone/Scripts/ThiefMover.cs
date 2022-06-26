using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobMaster3D
{
    public class ThiefMover
    {
        ThiefController _thiefController;
        public ThiefMover(ThiefController thiefController)
        {
            _thiefController = thiefController;
        }
        public void Move(float speed)
        {
            _thiefController.transform.position += _thiefController.transform.forward * Time.deltaTime * speed;
        }
    }
}

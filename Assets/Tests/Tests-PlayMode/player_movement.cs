using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class player_movement
    {
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator with_positive_vertical_input_moves_forward()
        {
            GameObject PlayerGameObjcet = new GameObject();
            Player player =  PlayerGameObjcet.AddComponent<Player>();
            player.PlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput.Vertical.Returns(1f);

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(PlayerGameObjcet.transform);
            cube.transform.localPosition = Vector3.zero;
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
            yield return new WaitForSeconds(0.3f);
            Assert.IsTrue(player.transform.position.z > 1f);
            Assert.AreEqual(0,player.transform.position.x);
            Assert.AreEqual(0, player.transform.position.y);
        }
    }
}

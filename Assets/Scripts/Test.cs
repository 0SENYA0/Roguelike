// <<<<<<< HEAD
// using DefaultNamespace.Tools;
// =======
// using System;
// using Assets.Infrastructure;
// using Assets.Scripts;
// using UnityEditor;
// >>>>>>> Tested
// using UnityEngine;
//
// namespace DefaultNamespace
// {
//     public class Test : MonoBehaviour
//     {
// <<<<<<< HEAD
//         [SerializeField] private Transform _capsule;
//         [SerializeField] private Transform _box;
//
//         [SerializeField] private Transform _circle;
//         [SerializeField] private Transform _circle2;
//         
//         private void Start()
//         {
//             Debug.Log("capsule" + _capsule);
//             Debug.Log("circle" + _circle);
//             Debug.Log("box" + _box);
//             
//             print("before _circle.position = " + _circle.position);
//             
//             _circle.position = _circle.TransformPoint(_box.position);
//             
//             print("after _circle.position  = " + _circle.position);
//             print("after _circle.position  = " + _circle.localPosition);
//             
// =======
//         private void OnEnable()
//         {
//             GameRoot.Instance.Sound.Pause();
//         }
//
//         private void OnDisable()
//         {
//             GameRoot.Instance.Sound.UpPause();
// >>>>>>> Tested
//         }
//     }
// }
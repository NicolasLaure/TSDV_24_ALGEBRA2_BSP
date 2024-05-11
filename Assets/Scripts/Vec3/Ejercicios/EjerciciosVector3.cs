using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using TMPro;

namespace ActividadEjercicios
{
    public class EjerciciosVector3 : MonoBehaviour
    {
        #region Variables

        #region UserInputs
        [SerializeField] private Ejercicios selectedExercise = Ejercicios.Uno;
        [SerializeField] private Color VectorColor = Color.red;
        [SerializeField] private Vec3 A;
        [SerializeField] private Vec3 B;
        [SerializeField] private GameObject linePrefab;
        #endregion

        private Ejercicios prevFrameSelectedExercise = Ejercicios.Uno;
        private Vec3 resultVector = new Vec3(0, 0, 0);
        private LineRenderer vectorA_LR;
        private LineRenderer vectorB_LR;
        private LineRenderer vectorResult_LR;
        #endregion
        private void Start()
        {
            InitializeLine(out vectorA_LR, "Vector A", Color.white);
            InitializeLine(out vectorB_LR, "Vector B", Color.black);
            InitializeLine(out vectorResult_LR, "Vector Result", VectorColor);

            SetVector(A, ref vectorA_LR);
            SetVector(B, ref vectorB_LR);
            ResolveExercise();
            prevFrameSelectedExercise = selectedExercise;
        }

        private void Update()
        {
            if (prevFrameSelectedExercise != selectedExercise)
            {
                ResolveExercise();
                prevFrameSelectedExercise = selectedExercise;
            }
        }

        private void ResolveExercise()
        {
            switch (selectedExercise)
            {
                case Ejercicios.Uno:
                    resultVector = A + B;
                    break;
                case Ejercicios.Dos:
                    resultVector = B - A;
                    break;
                case Ejercicios.Tres:
                    resultVector = A;
                    resultVector.Scale(B);
                    break;
                case Ejercicios.Cuatro:
                    resultVector = Vec3.Cross(B, A);
                    break;
                case Ejercicios.Cinco:
                    StartCoroutine(EjercicioCinco());
                    break;
                case Ejercicios.Seis:
                    resultVector = Vec3.Max(A, B);
                    break;
                case Ejercicios.Siete:
                    resultVector = Vec3.Project(A, B);
                    break;
                case Ejercicios.Ocho:
                    resultVector = Vec3.Lerp(A, B, 0.5f);
                    resultVector.Normalize();
                    resultVector *= Vec3.Distance(A,B);
                    break;
                case Ejercicios.Nueve:
                    resultVector = Vec3.Reflect(A, new Vec3(B.normalized));
                    break;
                case Ejercicios.Diez:
                    StartCoroutine(EjercicioDiez());
                    break;
                default:
                    break;
            }
            SetVector(resultVector, ref vectorResult_LR);
        }

        #region LineRendering
        private void InitializeLine(out LineRenderer line, string name, Color color)
        {
            GameObject spawnedLine = Instantiate(linePrefab, this.transform);

            spawnedLine.name = name;
            line = spawnedLine.GetComponent<LineRenderer>();
            line.startColor = color;
            line.endColor = color;
        }

        private void SetVector(Vec3 vector, ref LineRenderer line)
        {
            line.SetPosition(1, vector);
            string vectorText = @$"{line.gameObject.name}:
            X: {vector.x}
            Y: {vector.y}
            Z: {vector.z}";
            Transform textGO = line.gameObject.transform.GetChild(0);
            textGO.position = vector;
            TextMeshPro tmpText = textGO.GetComponent<TextMeshPro>();
            tmpText.text = vectorText;
            tmpText.color = line.startColor;
        }
        #endregion

        private IEnumerator EjercicioCinco()
        {
            while (selectedExercise == Ejercicios.Cinco)
            {
                float startTime = Time.time;
                float timer = 0;
                float duration = 1;
                while (timer < duration && selectedExercise == Ejercicios.Cinco)
                {
                    timer = Time.time - startTime;
                    resultVector = Vec3.Lerp(A, B, timer / duration);
                    SetVector(resultVector, ref vectorResult_LR);
                    yield return null;
                }
            }
        }
        private IEnumerator EjercicioDiez()
        {
            while (selectedExercise == Ejercicios.Diez)
            {
                float startTime = Time.time;
                float timer = 0;
                float durationZeroToOne = 1;
                float loopDuration = 10;
                while (timer < loopDuration && selectedExercise == Ejercicios.Diez)
                {
                    timer = Time.time - startTime;
                    resultVector = Vec3.LerpUnclamped(B, A, timer / durationZeroToOne);
                    SetVector(resultVector, ref vectorResult_LR);
                    yield return null;
                }
            }
        }
    }
}
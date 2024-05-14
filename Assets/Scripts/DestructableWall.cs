using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DestructableWall : MonoBehaviour
{
    [SerializeField] private float cubeSize = 0.3f;
    [SerializeField] private int cubesInRow = 3;

    private float cubesPivotDistance;
    private Vector3 cubesPivot;

    [SerializeField] private float explosionForce = 50f;
    [SerializeField] private float explosionRadius = 4f;
    [SerializeField] private float explosionUpward = 0.4f;

    private Material material;

    [Inject] private VfxManager vfxManager;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private void Explode(Collider playerCollider)
    {
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePieces(x, y, z, playerCollider);
                }
            }
        }

        Vector3 explosionPos = playerCollider.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb.GetComponent<Collider>() != playerCollider)
                {
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, explosionUpward);
                }
            }
        }

        Destroy(gameObject);
    }
    private void CreatePieces(int x, int y, int z, Collider playerCollider)
    {
        GameObject piece = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));

        if (piece != null)
        {
            piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
            piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            piece.SetActive(true);
        }
        piece.AddComponent<Rigidbody>();
        Rigidbody rbPiece = piece.GetComponent<Rigidbody>();
        rbPiece.mass = cubeSize;
        rbPiece.interpolation = RigidbodyInterpolation.Interpolate;
        rbPiece.solverIterations = 1;

        MeshRenderer meshRenderer = piece.GetComponent<MeshRenderer>();

        meshRenderer.material = material;

        Physics.IgnoreCollision(piece.GetComponent<Collider>(), playerCollider, true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Explode(other);

            vfxManager.PlayVFX(VfxManager.VfxType.hit, transform.position, Quaternion.identity);
        }
    }
}

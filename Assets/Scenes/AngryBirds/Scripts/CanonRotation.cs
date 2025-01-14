using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation=new(0,1);
    public Vector3 _minRotation= new(1,0);
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    public float initialScaleX;
/// <summary>
/// 
/// </summary>
    public Vector2 mousePosition;
    public Vector2 dist;
    public float degrees;
    public quaternion quaternion;
    bool coolDown= false;
    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        /*var mousePos = //guardem posici� del ratol� a la c�mera
        var direction = //vector entre el click i la bala
        var angle = (Mathf.Atan2(dist.y, dist.x) * 180f / Mathf.PI + offset);
        transform.rotation = Quaternion.Euler( //aplicar rotaci� des l'angle al can�  */
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dist = (mousePosition-(Vector2)transform.position).normalized;

        degrees =Mathf.Atan2(dist.y, dist.x)+ offset*Mathf.Rad2Deg + 90f;

        quaternion = quaternion.Euler(new Vector3(0,0,degrees));
        transform.rotation =quaternion;

        if (Input.GetMouseButton(0))
        {
            if(!coolDown)
            StartCoroutine(CoolDown());

        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject projectile = Instantiate(Bullet,ShootPoint.transform.position,quaternion.identity); //On s'instancia?
            projectile.GetComponent<Rigidbody2D>().linearVelocity= ProjectileSpeed*dist; //quina velocitat ha de tenir la bala? 
            
            ProjectileSpeed = 0f; //reset despr�s del tret
        }
        CalculateBarScale();

    }
    IEnumerator CoolDown(){
ProjectileSpeed+=4;
coolDown = true;
        yield return new WaitForSeconds(1);
        coolDown=false;
    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}

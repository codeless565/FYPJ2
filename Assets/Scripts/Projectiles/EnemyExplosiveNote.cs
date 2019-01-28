using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiveNote : MonoBehaviour, IProjectile
{
    bool m_initialized;
    float m_damage;
    float m_speed;
    float m_travelDist;
    Vector2 m_direction;
    Vector2 m_startPos;

    ProjectileType m_projectileType;
    AttackType m_attackType;

    //Unique members
    GameObject m_NormalNote;

    public void Init(float _damage, float _speed, float _travelDist, Vector2 _direction, ProjectileType _projectileType = ProjectileType.Normal, AttackType _attackType = AttackType.Basic)
    {
        m_initialized = true;
        m_damage = _damage;
        m_speed = _speed;
        m_travelDist = _travelDist;
        m_direction = _direction;
        m_startPos = transform.position;

        m_projectileType = _projectileType;
        m_attackType = _attackType;

        m_NormalNote = (GameObject)Resources.Load("Projectiles/EnemyNote");
    }

    public void CheckTravelDist()
    {
        if (((Vector2)transform.position - m_startPos).magnitude >= m_travelDist)
            Destroy(gameObject);
    }

    public float GetDamage()
    {
        return m_damage;
    }

    public Vector2 GetDirection()
    {
        return m_direction;
    }

    public float GetSpeed()
    {
        return m_speed;
    }

    public Sprite GetSprite()
    {
        if (gameObject != null)
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        return null;
    }

    public ProjectileType GetProjectileType()
    {
        return m_projectileType;
    }

    public AttackType GetAttackType()
    {
        return m_attackType;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTravelDist();

        //transform.rotation = Quaternion.LookRotation(m_direction, Vector2.up);
        transform.Translate(m_direction * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Wall")
        {
            GameObject newBullet1 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet1.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(0, 1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet2 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet2.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, 1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet3 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet3.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, 0).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet4 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet4.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet5 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet5.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(0, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet6 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet6.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet7 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet7.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, 0).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet8 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet8.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, 1).normalized, ProjectileType.Normal, AttackType.Basic);

            Destroy(gameObject);
        }
        else if (_other.tag == "Player")
        {
            //Hit monster, deal damage and spawn 
            _other.GetComponent<IEntity>().IsDamaged(m_damage, m_attackType);
            GameObject newBullet1 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet1.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(0, 1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet2 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet2.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, 1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet3 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet3.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, 0).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet4 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet4.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(1, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet5 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet5.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(0, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet6 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet6.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, -1).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet7 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet7.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, 0).normalized, ProjectileType.Normal, AttackType.Basic);
            GameObject newBullet8 = Instantiate(m_NormalNote, transform.position, Quaternion.identity);
            newBullet8.GetComponent<IProjectile>().Init(m_damage, 20, m_travelDist, new Vector2(-1, 1).normalized, ProjectileType.Normal, AttackType.Basic);
            Destroy(gameObject);
        }
    }
}
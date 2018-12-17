using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingNote : MonoBehaviour, IProjectile
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
    float m_damagaDrop;

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
        m_damagaDrop = m_damage * 0.1f;
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
        if (_other.tag == "Player")
            return;

        Debug.Log("collided name: " + _other.tag);
        if (_other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (_other.tag == "Monster")
        {
            _other.GetComponent<IEnemy>().GetStats().HP -= m_damage;
            m_damage -= m_damagaDrop;
        }
    }
}
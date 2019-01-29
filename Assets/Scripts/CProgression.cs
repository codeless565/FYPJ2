using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProgression
{
    private static CProgression instance;
    public static CProgression Instance
    {
        get
        {
            if (instance == null)
                instance = new CProgression();
            return instance;
        }
    }

    private CProgression()
    {
    }

    /******************************
     * UNIVERSAL PROGRESSION SAVES
     *****************************/
    public void UpdateOverallSave(CPlayer _player)
    {
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }

    /******************************
    * UNIVERSAL PROGRESSION SAVES
    *****************************/
    public void UpdateDungeonProgression()
    {
        PlayerPrefs.SetInt("DungeonFloorMax", CTDungeon.Instance.currentFloor);
    }

    public void UpdateDungeonProgression(int _floorNum)
    {
        PlayerPrefs.SetInt("DungeonFloorMax", _floorNum);
    }

    public int GetMaxDungeonProgression()
    {
        if (PlayerPrefs.HasKey("DungeonFloorMax"))
            return PlayerPrefs.GetInt("DungeonFloorMax");

        return 1;
    }

    /***************************
     * PLAYER PROGRESSION SAVES
     **************************/
    public void UpdatePlayerSave(CPlayer _player)
    {
        //Stats
        PlayerPrefs.SetInt("PlayerStats_Level", _player.GetStats().Level);
        PlayerPrefs.SetFloat("PlayerStats_CurrEXP", _player.GetStats().EXP);
        PlayerPrefs.SetFloat("PlayerStats_MaxEXP", _player.GetStats().MaxEXP);
        PlayerPrefs.SetFloat("PlayerStats_EXPBoost", _player.GetStats().EXPBoost);

        PlayerPrefs.SetFloat("PlayerStats_CurrHP", _player.GetStats().HP);
        PlayerPrefs.SetFloat("PlayerStats_MaxHP", _player.GetStats().MaxHP);
        PlayerPrefs.SetFloat("PlayerStats_CurrSP", _player.GetStats().SP);
        PlayerPrefs.SetFloat("PlayerStats_MaxSP", _player.GetStats().MaxSP);
        PlayerPrefs.SetInt("PlayerStats_ATK", _player.GetStats().Attack);
        PlayerPrefs.SetInt("PlayerStats_DEF", _player.GetStats().Defense);
        PlayerPrefs.SetFloat("PlayerStats_PlayRate", _player.GetStats().PlayRate);
        PlayerPrefs.SetFloat("PlayerStats_MoveSpeed", _player.GetStats().MoveSpeed);

        //Items
        PlayerPrefs.SetInt("PlayerItems_Notes", _player.m_InventorySystem.Notes);
        PlayerPrefs.SetInt("PlayerItems_Gems", _player.m_InventorySystem.Gems);
        PlayerPrefs.SetInt("PlayerItems_HPRation", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.HPRation.GetComponent<IItem>().ItemKey));
        PlayerPrefs.SetInt("PlayerItems_HPPotion", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.HPPotion.GetComponent<IItem>().ItemKey));
        PlayerPrefs.SetInt("PlayerItems_HPElixir", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.HPElixir.GetComponent<IItem>().ItemKey));
        PlayerPrefs.SetInt("PlayerItems_SPPotion", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.SPPotion.GetComponent<IItem>().ItemKey));
        PlayerPrefs.SetInt("PlayerItems_SPElixir", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.SPElixir.GetComponent<IItem>().ItemKey));
        PlayerPrefs.SetInt("PlayerItems_ReviveTix", _player.m_InventorySystem.GetItemQuantity(CItemDatabase.Instance.ReviveTix.GetComponent<IItem>().ItemKey));

        //Equipment
        PlayerPrefs.SetString("PlayerWeapon_Equipped", _player.EquippedWeapon.Name);

        //Quest
        for (int i = 0; i < 3; ++i)
        {
            if (PlayerPrefs.HasKey("PlayerQuestName" + i))
            {
                PlayerPrefs.DeleteKey("PlayerQuestName" + i);
                PlayerPrefs.DeleteKey("PlayerQuestCurrAmt" + i);
                PlayerPrefs.DeleteKey("PlayerQuestReqAmt" + i);
            }
        }

        for (int i = 0; i < _player.QuestList.Count; ++i)
        {
            PlayerPrefs.SetString("PlayerQuestName" + i, _player.QuestList[i].QuestString);
            PlayerPrefs.SetString("PlayerQuestType" + i, _player.QuestList[i].QuestType.ToString());
            PlayerPrefs.SetString("PlayerQuestTarget" + i, _player.QuestList[i].QuestTarget.ToString());

            PlayerPrefs.SetInt("PlayerQuestCurrAmt" + i, _player.QuestList[i].QuestAmount);
            PlayerPrefs.SetInt("PlayerQuestReqAmt" + i, _player.QuestList[i].QuestCompleteAmount);

            PlayerPrefs.SetInt("PlayerQuestReward" + i, _player.QuestList[i].QuestReward);
            PlayerPrefs.SetString("PlayerQuestRewardType" + i, _player.QuestList[i].QuestRewardType.ToString());
        }
    }

    public bool LoadPlayerSave(CPlayer _player)
    {
        //Stats
        if (PlayerPrefs.HasKey("PlayerStats_Level"))
        {
            _player.SetStats(
                 PlayerPrefs.GetInt("PlayerStats_Level"),
                 PlayerPrefs.GetFloat("PlayerStats_CurrEXP"),
                 PlayerPrefs.GetFloat("PlayerStats_MaxEXP"),
                 PlayerPrefs.GetFloat("PlayerStats_EXPBoost"),
                 PlayerPrefs.GetFloat("PlayerStats_CurrHP"),
                 PlayerPrefs.GetFloat("PlayerStats_MaxHP"),
                 PlayerPrefs.GetFloat("PlayerStats_CurrSP"),
                 PlayerPrefs.GetFloat("PlayerStats_MaxSP"),
                 PlayerPrefs.GetInt("PlayerStats_ATK"),
                 PlayerPrefs.GetInt("PlayerStats_DEF"),
                 PlayerPrefs.GetFloat("PlayerStats_PlayRate"),
                 PlayerPrefs.GetFloat("PlayerStats_MoveSpeed"));
        }
        else
            _player.SetStats(1, 0, 10, 1, 50, 50, 100, 100, 10, 10, 1, 5);

        //Items
        if (PlayerPrefs.HasKey("PlayerItems_Notes"))
        {
            _player.m_InventorySystem.Init(
                PlayerPrefs.GetInt("PlayerItems_Notes"),
                PlayerPrefs.GetInt("PlayerItems_Gems"),
                PlayerPrefs.GetInt("PlayerItems_HPRation"),
                PlayerPrefs.GetInt("PlayerItems_HPPotion"),
                PlayerPrefs.GetInt("PlayerItems_HPElixir"),
                PlayerPrefs.GetInt("PlayerItems_SPPotion"),
                PlayerPrefs.GetInt("PlayerItems_SPElixir"),
                PlayerPrefs.GetInt("PlayerItems_ReviveTix"));
        }
        else
            _player.m_InventorySystem.Init(0, 0, 0, 0, 0, 0, 0, 0);

        //Weapon
        if (PlayerPrefs.HasKey("PlayerWeapon_Equipped"))
        {
            switch (PlayerPrefs.GetString("PlayerWeapon_Equipped"))
            {
                case "TestWeapon":
                    _player.EquippedWeapon = new TestWeapon();
                    break;
                case "Melodica":
                    _player.EquippedWeapon = new Melodica();
                    break;
                case "Guitar":
                    _player.EquippedWeapon = new Guitar();
                    break;
                case "Recorder":
                    _player.EquippedWeapon = new Recorder();
                    break;
                    //case "Piano":
                    //    _player.EquippedWeapon = new Weapon_Piano();
                    //    break;
                    //case "Flute":
                    //    _player.EquippedWeapon = new Weapon_Flute();
                    //    break;
                    //case "Electric Guitar":
                    //    _player.EquippedWeapon = new Weapon_ElectricGuitar();
                    //    break;
                    //case "Harp":
                    //    _player.EquippedWeapon = new Harp();
                    //    break;
                    //case "Trumpet":
                    //    _player.EquippedWeapon = new Weapon_Trumpet();
                    //    break;
                    //case "Drum":
                    //    _player.EquippedWeapon = new Weapon_Drum();
                    //    break;
            }
        }
        else
            _player.EquippedWeapon = new Melodica();
        
        //Quest
        for (int i = 0; i < 3; ++i)
        {
            if (!PlayerPrefs.HasKey("PlayerQuestName" + i))
                continue;

            QuestBase newquest = new QuestBase();
            RewardType rewardType = RewardType.NONE;
            if (PlayerPrefs.GetString("PlayerQuestRewardType" + i).Contains(RewardType.NOTES.ToString()))
                rewardType = RewardType.NOTES;
            else if (PlayerPrefs.GetString("PlayerQuestRewardType" + i).Contains(RewardType.GEMS.ToString()))
                rewardType = RewardType.GEMS;



            if (PlayerPrefs.GetString("PlayerQuestType" + i).Contains(QuestType.REACH.ToString()))
            {
                newquest = new QuestBase(QuestType.REACH,
                    PlayerPrefs.GetInt("PlayerQuestCurrAmt" + i),
                    PlayerPrefs.GetInt("PlayerQuestReqAmt" + i),
                    PlayerPrefs.GetInt("PlayerQuestReward" + i),
                    rewardType);
            }
            else if (PlayerPrefs.GetString("PlayerQuestType" + i).Contains(QuestType.SLAY.ToString()))
            {
                QuestTarget questtarget = QuestTarget.NONE;
                if (PlayerPrefs.GetString("PlayerQuestTarget" + i).Contains(QuestTarget.NOISE.ToString()))
                    questtarget = QuestTarget.NOISE;


                newquest = new QuestBase(QuestType.REACH,
                    questtarget,
                    PlayerPrefs.GetInt("PlayerQuestCurrAmt" + i),
                    PlayerPrefs.GetInt("PlayerQuestReqAmt" + i),
                    PlayerPrefs.GetInt("PlayerQuestReward" + i),
                    rewardType);
            }

            _player.AddNewQuest(newquest);
        }


        return true;
    }

}

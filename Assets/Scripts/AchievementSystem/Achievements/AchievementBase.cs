using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AchievementBase {
    string AchievementName { get; }
    bool AchievementCompleted { get; set; }
    bool AchievementActive { get; set; }
    Dictionary<string, QPropertiesBase> PropertiesList { get; set; }
    int CompletedProperties { get; set; }

    void Init(CStats _playerstats);
    void CheckRequirement();
    void UpdateAchievement();
    void AchievementReward();
    void AddProperty(QPropertiesBase _property);
}

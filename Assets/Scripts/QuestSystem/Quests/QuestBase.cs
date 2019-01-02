using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface QuestBase {
    string QuestName { get; }
    bool QuestCompleted { get; set; }
    bool QuestActive { get; set; }
    Dictionary<string, QPropertiesBase> PropertiesList { get; set; }
    int CompletedProperties { get; set; }


    void Update();
    void QuestReward();
    void AddProperty(QPropertiesBase _property);
}

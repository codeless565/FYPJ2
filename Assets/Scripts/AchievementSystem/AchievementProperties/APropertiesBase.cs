using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface QPropertiesBase {
    string PropertyName{ get; }
    bool IsCompleted{ get; set; }
    bool IsActive { get; set; }
	float CurrentValue { get; set; }
    float CompleteValue { get; set; }

    void AddCurrentValue(float _amount);
    void SetCurrentValue(float _amount);
    void Update();
}

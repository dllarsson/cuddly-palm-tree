using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usage examples:

// Normal selector to pick from all tags:
// [TagSelector] public string tag;

// Default tag
// [TagSelector] public string tag = "Enemy";

// Multiple tags (in array)
// [TagSelector] public string[];

// Multiple tags with default values
// [TagSelector] public string[] = { "Untagged", "EditorOnly" };


//Picking <All Tags> will return an empty string.

public class TagSelectorTestScript : MonoBehaviour
{
    [TagSelector]
    [SerializeField]
    string singleTag;

    //Default value
    [TagSelector]
    [SerializeField]
    string singleTagDefaultValue = "Enemy";

    [TagSelector]
    [SerializeField]
    string[] arrayTags;

    //Defaults 2 values
    [TagSelector]
    [SerializeField]
    string[] arrayTagsDefaultValue = { "Enemy", "Untagged" };
}

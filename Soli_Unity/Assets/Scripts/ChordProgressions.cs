﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class ChordProgressions : MonoBehaviour
    {
        DiatonicScales diatonicScales;
        public GameController game_c;
        public GestureController gesture_c;

        [Header("Synth Section")]
        public AudioHelm.HelmController DroneSynth;
        public AudioHelm.Sampler Piano;
        [Header("Sequencer Section")]
        public AudioHelm.Sequencer DroneSeq;
        public int droneSeqPos;
        [Header("Key,Scale & Chord Picker")]
        public string Key, KeyType, ChordVoicing, ChordType;
        public bool[] chords = new bool[8];
        [Header("Level Picker")]
        public bool Run, Level1, Level2, Level3;
        [Header("NoteParameters")]
        public float f_noteVelocity;

        // Use this for initialization
        void Start()
        {
            diatonicScales = this.GetComponent<DiatonicScales>();
        }

        // Update is called once per frame
        void Update()
        {
            droneSeqPos = (int)DroneSeq.GetSequencerPosition();
            f_noteVelocity = gesture_c.f_NoteVelocity;

            diatonicScales.MajorScales(Key);
            diatonicScales.NatMinorScales(Key);
            SamplerEnable();

            if (chords[1] || chords[2] || chords[3] || chords[4] || chords[5] || chords[6] || chords[7])
            {
                DroneEnable();                
            }
        }

        public void DroneEnable()
        {
            DroneSynth.AllNotesOff();
            DroneSeq.Clear();
        
            for (int i = 0; i < chords.Length; i++)
            {
                if (chords[i])
                {
                    switch (i)
                    {
                        case 1: //CHORD I 

                            if (KeyType == "Major") //Ionian 
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom maj7(6)
                                {
                                    
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9                              
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                } else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Aeolian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3                              

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 2: //CHORD IImin7, II7, 

                            if (KeyType == "Major") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1

                                if (ChordType == "NonDiatonic") //II7
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4] + 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                }

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom
                                {

                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                            {
                                
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1                                
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //11
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                }
                            }
                            break;

                        case 3: // CHORD III-7, IIImaj7, III7

                            if (KeyType == "Major") // Phrygian  
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // min7b911 voicing 7 on bottom
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6(11)
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Ionian Maj7(6)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                
                                if (ChordVoicing == "Extended")
                                {
                                    if (ChordType == "NonDiatonic") //7
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3] - 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 4: // CHORD IV 

                            if (KeyType == "Major") //Lydian 
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") //maj7 voicing 3rd on bottom
                                {

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //#11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 5: // CHORD V7, V7alt(b5,b9), V-7

                            if (KeyType == "Major") // Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // dom7(9) voicings with 7 on bottom 73695
                                {
                                    if(ChordType == "NonDiatonic") //alt
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[2] - 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b5
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6] - 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b9
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale4[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Phrygian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //min7(9)
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 6: // CHORD VI-7, VImaj7, VI7  

                            if (KeyType == "Major") //Aeolian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // min7(9) voicing 3  at bottom 3795
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Lydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //maj7 and 7
                                {
                                    if (ChordType == "NonDiatonic")
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5] - 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7 
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6 
                                    }

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 7: // CHORD VII0, VIIo, VII-7, VII7

                            if (KeyType == "Major") //Locrian 
                            {

                                DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b5

                                if (ChordVoicing == "Extended") //min7b5 voicings 7 on bottom 3,5,1,11,7
                                {
                                    if(ChordType == "NonDiatonic") //VIIO
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //bb7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1

                                if (ChordVoicing == "Extended") //dom7 73695
                                {
                                    if (ChordType == "NonDiatonic") //min7
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2] - 1, droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;
                    }

                    chords[i] = false;
                }
            }
        }

        public void SamplerEnable()
        {
           
         if(game_c.Focus)
            {
                for (int i = 0; i < chords.Length; i++)
                {
                    if (chords[i])
                    {
                        switch (i)
                        {
                            case 1: //CHORD I 

                                if (KeyType == "Major") //Ionian 
                                {

                                    Piano.NoteOn(diatonicScales.Major_Scale1[1], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.Major_Scale2[3], f_noteVelocity); //3

                                    if (ChordVoicing == "Extended") // voicings with 3rd on bottom maj7(6)
                                    {

                                        Piano.NoteOn(diatonicScales.Major_Scale2[6], f_noteVelocity); //6
                                        Piano.NoteOn(diatonicScales.Major_Scale3[2], f_noteVelocity); //9                              
                                        Piano.NoteOn(diatonicScales.Major_Scale3[5], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[5], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") // Aeolian min7(9)
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[1], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[3], f_noteVelocity); //3                              

                                    if (ChordVoicing == "Extended")
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[7], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[2], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[5], f_noteVelocity); //5
                                    }
                                    else
                                    {

                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[5], f_noteVelocity); //5
                                    }
                                }
                                break;

                            case 2: //CHORD IImin7, II7, 

                                if (KeyType == "Major") //Dorian min7(9)
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[2], f_noteVelocity); //1

                                    if (ChordType == "NonDiatonic") //II7
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[4] + 1, f_noteVelocity); //3
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[4], f_noteVelocity); //b3
                                    }

                                    if (ChordVoicing == "Extended") // voicings with 3rd on bottom
                                    {

                                        Piano.NoteOn(diatonicScales.Major_Scale2[1], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.Major_Scale3[3], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.Major_Scale3[6], f_noteVelocity); //5

                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[6], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                                {

                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[4], f_noteVelocity); //3
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[6], f_noteVelocity); //5

                                    if (ChordVoicing == "Extended")
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[2], f_noteVelocity); //1                                
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[5], f_noteVelocity); //11
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale4[1], f_noteVelocity); //7
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale1[2], f_noteVelocity); //1
                                    }
                                }
                                break;

                            case 3: // CHORD III-7, IIImaj7, III7

                                if (KeyType == "Major") // Phrygian  
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[3], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.Major_Scale2[5], f_noteVelocity); //3

                                    if (ChordVoicing == "Extended") // min7b911 voicing 7 on bottom
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[2], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.Major_Scale3[1], f_noteVelocity); //6(11)
                                        Piano.NoteOn(diatonicScales.Major_Scale3[4], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.Major_Scale3[7], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[7], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") // Ionian Maj7(6)
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[3], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[5], f_noteVelocity); //3

                                    if (ChordVoicing == "Extended")
                                    {
                                        if (ChordType == "NonDiatonic") //7
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale3[3] - 1, f_noteVelocity); //b7
                                        }
                                        else
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale3[2], f_noteVelocity); //6
                                        }
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[4], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[7], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[7], f_noteVelocity); //5
                                    }
                                }
                                break;

                            case 4: // CHORD IV 

                                if (KeyType == "Major") //Lydian 
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[4], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.Major_Scale2[6], f_noteVelocity); //3

                                    if (ChordVoicing == "Extended") //maj7 voicing 3rd on bottom
                                    {

                                        //     Piano.NoteOn(diatonicScales.Major_Scale3[3], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.Major_Scale3[2], f_noteVelocity); //6
                                        Piano.NoteOn(diatonicScales.Major_Scale3[5], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.Major_Scale4[1], f_noteVelocity); //5

                                        //     Piano.NoteOn(diatonicScales.Major_Scale3[7], f_noteVelocity); //#11

                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[1], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") //Dorian min7(9)
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[4], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[6], f_noteVelocity); //3


                                    if (ChordVoicing == "Extended")
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[3], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[5], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale4[1], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[1], f_noteVelocity); //5
                                    }
                                }
                                break;

                            case 5: // CHORD V7, V7alt(b5,b9), V-7

                                if (KeyType == "Major") // Mixolydian
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[5], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.Major_Scale2[7], f_noteVelocity); //3


                                    if (ChordVoicing == "Extended") // dom7(9) voicings with 7 on bottom 73695
                                    {
                                        if (ChordType == "NonDiatonic") //alt
                                        {
                                            Piano.NoteOn(diatonicScales.Major_Scale3[2] - 1, f_noteVelocity); //b5
                                            Piano.NoteOn(diatonicScales.Major_Scale3[6] - 1, f_noteVelocity); //b9
                                        }
                                        else
                                        {
                                            Piano.NoteOn(diatonicScales.Major_Scale4[2], f_noteVelocity); //5
                                            Piano.NoteOn(diatonicScales.Major_Scale3[6], f_noteVelocity); //9
                                            Piano.NoteOn(diatonicScales.Major_Scale3[3], f_noteVelocity); //6
                                        }
                                        Piano.NoteOn(diatonicScales.Major_Scale2[4], f_noteVelocity); //7
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[2], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") //Phrygian
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[5], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[7], f_noteVelocity); //3


                                    if (ChordVoicing == "Extended") //min7(9)
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[4], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[3], f_noteVelocity); //6
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[6], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale4[2], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[2], f_noteVelocity); //5
                                    }
                                }
                                break;

                            case 6: // CHORD VI-7, VImaj7, VI7  

                                if (KeyType == "Major") //Aeolian
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[6], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.Major_Scale2[1], f_noteVelocity); //3


                                    if (ChordVoicing == "Extended") // min7(9) voicing 3  at bottom 3795
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[5], f_noteVelocity); //7
                                        Piano.NoteOn(diatonicScales.Major_Scale2[7], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.Major_Scale3[3], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale2[3], f_noteVelocity); //5
                                    }
                                }
                                else if (KeyType == "NaturalMinor") //Lydian
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[6], f_noteVelocity); //1
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[1], f_noteVelocity); //3


                                    if (ChordVoicing == "Extended") //maj7 and 7
                                    {
                                        if (ChordType == "NonDiatonic")
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale2[5] - 1, f_noteVelocity); //b7 
                                        }
                                        else
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale2[4], f_noteVelocity); //6 
                                        }

                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[7], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[3], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[3], f_noteVelocity); //5
                                    }
                                }
                                break;

                            case 7: // CHORD VII0, VIIo, VII-7, VII7

                                if (KeyType == "Major") //Locrian 
                                {

                                    Piano.NoteOn(diatonicScales.Major_Scale2[2], f_noteVelocity); //b3
                                    Piano.NoteOn(diatonicScales.Major_Scale2[4], f_noteVelocity); //b5

                                    if (ChordVoicing == "Extended") //min7b5 voicings 7 on bottom 3,5,1,11,7
                                    {
                                        if (ChordType == "NonDiatonic") //VIIO
                                        {
                                            Piano.NoteOn(diatonicScales.Major_Scale3[6], f_noteVelocity); //bb7
                                        }
                                        else
                                        {
                                            Piano.NoteOn(diatonicScales.Major_Scale3[6], f_noteVelocity); //b7
                                        }
                                        Piano.NoteOn(diatonicScales.Major_Scale2[7], f_noteVelocity); //1
                                        Piano.NoteOn(diatonicScales.Major_Scale3[3], f_noteVelocity); //11

                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.Major_Scale1[7], f_noteVelocity); //1
                                    }
                                }
                                else if (KeyType == "NaturalMinor") //Mixolydian
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[7], f_noteVelocity); //1

                                    if (ChordVoicing == "Extended") //dom7 73695
                                    {
                                        if (ChordType == "NonDiatonic") //min7
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale3[2] - 1, f_noteVelocity); //b3
                                        }
                                        else
                                        {
                                            Piano.NoteOn(diatonicScales.NatMinor_Scale3[2], f_noteVelocity); //3
                                        }
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[6], f_noteVelocity); //b7
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[1], f_noteVelocity); //9
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale3[4], f_noteVelocity); //5
                                    }
                                    else
                                    {
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[2], f_noteVelocity); //3
                                        Piano.NoteOn(diatonicScales.NatMinor_Scale2[4], f_noteVelocity); //5
                                    }
                                }
                                break;
                        }

                        //   chords[i] = false;
                    }
                }
            }
         else
            {
                Piano.AllNotesOff();
            }
        }

    }
}

import essentia
from essentia.standard import *
import numpy as np

fname = './beat-test.wav'
audiobeat = MonoLoader(filename=fname)()

rhythm_extractor = RhythmExtractor2013(method="multifeature")
bpm, beats, b_conf, _, _ = rhythm_extractor(audiobeat)

# remove uncomment to print bpm, list of beats, & b_conf to console
# print("BPM: ", bpm)
# print("Beat Positions (sec.): ", beats)
# print("Beat Estimation Confidence: ", b_conf)

f = open("./beat-test.txt", "w+")
f.write(str(beats).replace('[',' ').replace(']',''))
f.close()


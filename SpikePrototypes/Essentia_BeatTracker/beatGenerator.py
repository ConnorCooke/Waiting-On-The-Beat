import essentia
from essentia.standard import *
import numpy as np
import json

fname = './beat-test.wav'
audiobeat = MonoLoader(filename=fname)()

rhythm_extractor = RhythmExtractor2013(method="multifeature")
bpm, beats, b_conf, _, _ = rhythm_extractor(audiobeat)

# remove uncomment to print bpm, list of beats, & b_conf to console
# print("BPM: ", bpm)
# print("Beat Positions (sec.): ", beats)
# print("Beat Estimation Confidence: ", b_conf)

# beats is a numpy array filled with numpy.float32
# converted to floats by list comprehension stored in f_beats
f_beats = [float(np_float) for np_float in list(beats)]

with open("./beat-test.txt", "w+") as writer:
	writer.write('\n'.join(str(x) for x in f_beats))


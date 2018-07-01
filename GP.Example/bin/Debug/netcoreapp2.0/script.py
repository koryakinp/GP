import matplotlib.pyplot as plt
from matplotlib import gridspec
import numpy as np
import codecs, json, sys

def from_json(filename, arr_type):
    obj_text = codecs.open(filename, 'r', encoding='utf-8').read()
    b_new = json.loads(obj_text)
    arr = np.array(b_new)
    return np.core.records.fromarrays(arr.transpose(), dtype=arr_type)

def plot(predicted, observed, aquistition):
    fig = plt.figure(figsize=(16, 9)) 
    fig.subplots_adjust(wspace=0, hspace=0)
    gs = gridspec.GridSpec(2, 1, height_ratios=[3, 1]) 
    ax0 = plt.subplot(gs[0], title="Posterior distribution")
    ax0.plot(predicted['x'], predicted['mean'])
    ax0.plot(observed['x'], observed['y'], 'o', ms=5, color='black')
    ax0.fill_between(predicted['x'], predicted['lower'], predicted['upper'], color="lightblue")

    x_min = predicted['x'].min()
    x_max = predicted['x'].max()
    y_max = predicted['upper'].max() + 0.05 * (predicted['upper'].max() - predicted['lower'].min())
    y_min = predicted['lower'].min() - 0.05 * (predicted['upper'].max() - predicted['lower'].min())

    ax0.axis([x_min, x_max, y_min, y_max])
    ax1 = plt.subplot(gs[1], title="Aquisition Function")
    ax1.axis([aquistition['x'].min(), aquistition['x'].max(), 0, aquistition['y'].max() * 1.05])
    ax1.plot(aquistition['x'], aquistition['y'])
    ax1.fill_between(aquistition['x'], aquistition['y'], color="orange")
    plt.show()
    
predicted_dt =[('mean','d'),('upper','d'),('lower','d'),('x','d')] 
observed_dt = [('x','d'),('y','d')]

predicted = from_json(sys.argv[1], predicted_dt)
observed = from_json(sys.argv[2], observed_dt)
aquistition = from_json(sys.argv[3], observed_dt)

plot(predicted, observed, aquistition)
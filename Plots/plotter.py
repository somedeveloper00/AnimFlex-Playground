import matplotlib.pyplot as plt
import numpy as np
import sys

data = open(sys.argv[1]).read()
a = [ float(i) for i in data.split(', ')]

plt.plot(a)
plt.ylabel('MilliSeconds')
plt.xlabel('frame')
plt.show()
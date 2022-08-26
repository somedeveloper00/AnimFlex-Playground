import matplotlib.pyplot as plt
import numpy as np
import sys

data = sys.argv[1]
x = [ float(i) for i in data.split(', ')]

y = [ float(i) for i in sys.argv[2].split(', ')]

xlabel = sys.argv[3]
ylabel = sys.argv[4]

print(len(x), len(y))
plt.plot(y, x)
plt.ylabel('MilliSeconds')
plt.xlabel('frame')
plt.show()
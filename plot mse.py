import re
import matplotlib.pyplot as plt
import numpy as np
import sys
import json


replacements = {
    'sin' : 'np.sin',
    'cos' : 'np.cos',
    'tan':"np.tan",
    'exp': 'np.exp',
    'sqrt': 'np.sqrt',
    '^': '**',
    "pi": 'np.pi',
}

allowed_words = [
    'x',
    'sin',
    'cos',
    "tan",
    'sqrt',
    'exp',
    "pi"
]

def string2func(string):
    ''' evaluates the string and returns a function of x '''
    # find all words and check if all are allowed:
    for word in re.findall('[a-zA-Z_]+', string):
        if word not in allowed_words:
            raise ValueError(
                '"{}" is forbidden to use in math expression'.format(word)
            )

    for old, new in replacements.items():
        string = string.replace(old, new)

    def func(x):
        return eval(string)

    return func


if __name__ == '__main__':
    ys=None
    n=None
    func_str=None

    if len(sys.argv)>1:
        inp=sys.argv[1]
        ys = json.loads(inp)
        # ys=map(float, inp.strip('[]').split(','))
        n=len(ys)
    # else:
    #     func_str=input('enter function: f(x) = ')
    #     func = string2func(func_str)
    xPoints = []
    yPoints = []
    for x in range(1,n+1):
        y =ys[x-1]
        xPoints.append(x)
        yPoints.append(y)
    plt.title("mse:")
    plt.plot(xPoints, yPoints)
    plt.show()
    # a = float(input('enter lower limit: '))
    # b = float(input('enter upper limit: '))
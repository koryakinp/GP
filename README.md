# .NET Core implementation of Gaussian Processes

### Exploration:
```
var kernel = new GaussianKernel(0.25, 1);
var model = new Model(kernel, 0, 8, 800, ObjectiveFunction);
var output = model.Explore(14);
```
![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gifs/gp-explore.gif?raw=true)
### Search For Min/Max:

#### Expected Improvement Utility Function:
![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gifs/latex.png?raw=true)
```
var kernel = new GaussianKernel(0.25, 1);
var model = new Model(kernel, 0, 8, 800, ObjectiveFunction);
var output = model.FindExtrema(Goal.Max, 14);
```
![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gifs/gp-max-ei.gif?raw=true)

## Authors

Pavel koryakin <koryakinp@koryakinp.com>

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/koryakinp/GP/blob/master/LICENSE) for details.

## Acknowledgments

- [Nando de Freitas, Machine learning - Gaussian processes](https://www.youtube.com/watch?v=MfHKW5z-OOA)
- [Katherine Bailey, Gaussian Processes for Dummies](http://katbailey.github.io/post/gaussian-processes-for-dummies/)

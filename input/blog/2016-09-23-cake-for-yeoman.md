---
title: Announcing Cake for Yeoman
category: News
author: agc93
---

# Announcing Cake for Yeoman

Yeoman users rejoice! You can now bootstrap your project for Cake with [Yeoman](http://yeoman.io) and the new `generator-cake` generator.

<!--excerpt-->

If you have [node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/) already installed, simply run the following commands to quickly get started with Yeoman and Cake:

```
npm install -g yo
npm install -g generator-cake
```

Then, from your terminal, just run `yo cake` to quickly bootstrap Cake, including a build script, bootstrapper scripts and config files in the current folder.

![Running 'yo cake'](/assets/img/cake-for-yeoman/yo-cake.gif)

You can also install each of these individually using `yo cake:config` or `yo cake:bootstrapper`.

## Frosting

Finally, you can also use our (experimental) generator for [Frosting](https://github.com/cake-build/frosting) to quickly setup a new .NET Core project using Frosting:

```
yo cake:frosting
```

![Running 'yo cake:frosting'](/assets/img/cake-for-yeoman/yo-frosting.gif)

You can find the source [on GitHub](https://github.com/agc93/generator-cake) and I'm happy to take contributions, questions and feedback on the generator.

---

My name is Alistair Chapman and I'm an ALM consultant and occasional .NET developer from Brisbane, Australia who started working on Cake in 2016 and coincidentally hasn't been bored ever since. You can find me on [GitHub](https://github.com/agc93) and [Twitter](https://twitter.com/agc93) as agc93, or check out [my blog](http://blog.agchapman.com).
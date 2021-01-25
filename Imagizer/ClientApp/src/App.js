import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Settings } from './components/Settings';
import { SplashScreen } from './components/SplashScreen';
import { Authentication } from './components/authentication/Authentication';

import { Container } from 'reactstrap';
import './custom.css'

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Switch>
				<Route exact path='/' component={SplashScreen} />
				<Route exact path='/authentication' component={Authentication} />
				<Route children={() =>
					<Layout>
						<Route exact path='/home' component={Home} />
						<Route path='/settings' component={Settings} />
					</Layout>
				} />
			</Switch>
		);
	}
}

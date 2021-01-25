import React, { Component } from 'react';
import { Spinner, Row, Col } from 'reactstrap'
import { Redirect } from 'react-router-dom';


export class SplashScreen extends Component {
	static displayName = SplashScreen.name;

	static signInStates = {
		LOADING: 'LOADING',
		OK: 'OK',
		ERROR: 'ERROR',
		NOT_AUTHORIZED: 'NOT_AUTHORIZED'
	};

	constructor(props) {
		super(props)

		this.state = {
			signInState: SplashScreen.signInStates.LOADING
		}
	}

	async componentDidMount() {
		await this.isSignedIn()
	}

	async isSignedIn() {
		const result = await fetch('/api/v1/user/isSignedIn');

		let newState = SplashScreen.signInStates.ERROR;

		if (result.ok) {
			newState = SplashScreen.signInStates.OK;
		} else if (result.status === 401) {
			newState = SplashScreen.signInStates.NOT_AUTHORIZED;
		} else {
			newState = SplashScreen.signInStates.ERROR;
		}

		this.setState({
			signInState: newState
		});
	}

	render() {
		switch (this.state.signInState) {
			case SplashScreen.signInStates.LOADING:
				return (
					<Row>
						<Col xs='6' />
						<Col xs='auto'>
							<Spinner style={{ width: '3rem', height: '3rem' }} type="grow" />
						</Col>
						<Col xs='6' />
					</Row>
				);
			case SplashScreen.signInStates.ERROR:
				return (<h1>An error happened. Please try again later.</h1>);
			case SplashScreen.signInStates.OK:
				return (<Redirect to="/home" />);
			case SplashScreen.signInStates.NOT_AUTHORIZED:
				return (<Redirect to="/authentication" />);
		}
	}
}

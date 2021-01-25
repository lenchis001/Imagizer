import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Form, FormGroup, Input, Button } from 'reactstrap'


export class SignIn extends Component {
	static displayName = SignIn.name;

	static signingInState = {
		FILLING: 'FILLING',
		SIGNED_IN: 'SIGNED_IN'
	}

	constructor(props) {
		super(props)

		this.emailField = React.createRef();
		this.passwordField = React.createRef();

		this.state = {
			activeTab: 'signIn',
			signingInState: SignIn.signingInState.FILLING
		}

		this.onSignInRequested = this.onSignInRequested.bind(this);
	}

	async onSignInRequested(e) {
		e.preventDefault();

		const response = await fetch("/api/v1/user/signIn", {
			method: 'POST',
			body: JSON.stringify({
				email: this.emailField.current.value,
				password: this.passwordField.current.value
			}),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		if (response.ok) {
			this.setState({
				signingInState: SignIn.signingInState.SIGNED_IN
			})
		} else {
			alert("An error happened. Check data and try again.");
		}
	}

	render() {
		switch (this.state.signingInState) {
			case SignIn.signingInState.FILLING:
				return (
					<Form onSubmit={this.onSignInRequested}>
						<FormGroup row>
							<Input placeholder="Email" type="email" innerRef={this.emailField} />
						</FormGroup>
						<FormGroup row>
							<Input placeholder="Password" type="password" innerRef={this.passwordField} />
						</FormGroup>
						<FormGroup>
							<Button>Sign in</Button>
						</FormGroup>
					</Form>
				);
			case SignIn.signingInState.SIGNED_IN:
				return (<Redirect to="/home" />);
			default:
				return (<h3>An error happened. State is: {this.state.signingInState}</h3>);
		}
		
	}
}

import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Form, FormGroup, Input, Button } from 'reactstrap'


export class Register extends Component {
	static displayName = Register.name;

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
			signingInState: Register.signingInState.FILLING
		}

		this.onRegisterRequested = this.onRegisterRequested.bind(this);
	}

	async onRegisterRequested(e) {
		e.preventDefault();

		const response = await fetch("/api/v1/user/register", {
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
				signingInState: Register.signingInState.SIGNED_IN
			})
		} else {
			alert("An error happened. Check data and try again.");
		}
	}

	render() {
		switch (this.state.signingInState) {
			case Register.signingInState.FILLING:
				return (
					<Form onSubmit={this.onRegisterRequested}>
						<FormGroup row>
							<Input placeholder="Email" type="email" innerRef={this.emailField} />
						</FormGroup>
						<FormGroup row>
							<Input placeholder="Password" type="password" innerRef={this.passwordField} />
						</FormGroup>
						<FormGroup>
							<Button>Register</Button>
						</FormGroup>
					</Form>
				);
			case Register.signingInState.SIGNED_IN:
				return (<Redirect to="/home" />);
			default:
				return (<h3>An error happened. State is: {this.state.signingInState}</h3>);
		}
		
	}
}

import React, { Component } from 'react';
import { Nav, NavItem, NavLink, Spinner, Row, Col, TabContent, TabPane, Form, FormGroup, Input, Button } from 'reactstrap'
import { Redirect } from 'react-router-dom';
import { SignIn } from './forms/SignIn';
import { Register } from './forms/Register';
import classnames from 'classnames';


export class Authentication extends Component {
	static displayName = Authentication.name;

	constructor(props) {
		super(props)

		this.state = {
			activeTab: 'signIn'
		}

		this.toggle = this.toggle.bind(this);
	}

	toggle(tab) {
		if (this.state.activeTab !== tab)
			this.setState({
				activeTab: tab
			});
	}

	render() {
		return (
			<div>
				<Nav tabs>
					<NavItem>
						<NavLink
							className={classnames({ active: this.state.activeTab === 'signIn' })}
							onClick={() => { this.toggle('signIn'); }}
						>
							Sign In
						</NavLink>
					</NavItem>
					<NavItem>
						<NavLink
							className={classnames({ active: this.state.activeTab === 'register' })}
							onClick={() => { this.toggle('register'); }}
						>
							Register
						</NavLink>
					</NavItem>
				</Nav>
				<TabContent activeTab={this.state.activeTab}>
					<TabPane tabId="signIn">
						<SignIn/>
					</TabPane>
					<TabPane tabId="register">
						<Register/>
					</TabPane>
				</TabContent>
			</div>
		);
	}
}

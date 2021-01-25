import React, { Component } from 'react';
import { InputGroup, InputGroupAddon, Input, Button } from 'reactstrap'

export class Settings extends Component {
  static displayName = Settings.name;

  constructor(props) {
    super(props);
    this.state = {  };
  }

  render() {
    return (
        <div>
            <InputGroup>
                <Input placeholder="API Key" />
                <InputGroupAddon addonType="append">
                    <Button>Re-Generate</Button>
                </InputGroupAddon>
            </InputGroup>
      </div>
    );
  }
}

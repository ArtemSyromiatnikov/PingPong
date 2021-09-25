terraform {
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm"
        version = ">=2.78"
    }
  }
  backend "azurerm" {
    resource_group_name   = "ping-pong-infrastructure"
    storage_account_name  = "sapingpongterraform"
    container_name        = "terraform-state"
    key                   = "prod.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "ping-pong" {
  name      = "ping-pong"
  location  = "North Europe"
}

## DB Server configuration is commented out until we decide how to deal with sensitive data
# resource "azurerm_mssql_server" "dbs-pingpong" {
#   name                = "dbs-pingpong"
#   resource_group_name = azurerm_resource_group.ping-pong.name
#   location            = azurerm_resource_group.ping-pong.location
#   version             = "12.0"
#   administrator_login = "lord-admin"
#   administrator_login_password = "xxxxxxxx"    # SENSITIVE INFO!

#   minimum_tls_version = "1.2"
# }

resource "azurerm_mssql_database" "db-pingpong" {
  name = "db-pingpong"
  server_id = "/subscriptions/c4859b0f-5443-4e12-8915-bef7d646c730/resourceGroups/ping-pong/providers/Microsoft.Sql/servers/dbs-pingpong" # for now
  storage_account_type = "LRS"
}


// General config
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

// Variables and local
locals {
  db_server_id = "/subscriptions/c4859b0f-5443-4e12-8915-bef7d646c730/resourceGroups/ping-pong/providers/Microsoft.Sql/servers/dbs-pingpong"
}

// Data sources
data "azurerm_key_vault_secret" "dbs-server-password" {
  key_vault_id = "/subscriptions/c4859b0f-5443-4e12-8915-bef7d646c730/resourceGroups/ping-pong-infrastructure/providers/Microsoft.KeyVault/vaults/kv-pp-infrastructure"
  name = "dbs-admin-password"
}


resource "azurerm_resource_group" "ping-pong" {
  name      = "ping-pong"
  location  = "North Europe"
}

# azure will keep 'modifying' DB server because it thinks that password was modified (even though it wasn't)
resource "azurerm_mssql_server" "dbs-pingpong" {
  name                = "dbs-pingpong"
  resource_group_name = azurerm_resource_group.ping-pong.name
  location            = azurerm_resource_group.ping-pong.location
  version             = "12.0"
  administrator_login = "lord-admin"
  administrator_login_password = data.azurerm_key_vault_secret.dbs-server-password.value

  minimum_tls_version = "1.2"
}

resource "azurerm_mssql_database" "db-pingpong" {
  name = "db-pingpong"
  server_id = local.db_server_id
  storage_account_type = "LRS"
}


resource "azurerm_app_service_plan" "asp-pingpong" {
  name                = "asp-pingpong"
  resource_group_name = azurerm_resource_group.ping-pong.name
  location            = azurerm_resource_group.ping-pong.location
  kind                = "App"
  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "pingpong-api" {
  name                = "pingpong-api"
  app_service_plan_id = azurerm_app_service_plan.asp-pingpong.id
  resource_group_name = azurerm_resource_group.ping-pong.name
  location            = azurerm_resource_group.ping-pong.location

  //app_settings {
    //"Database" = azurerm_mssql_database.db-pingpong.
  //}
}

resource "azurerm_storage_account" "sapingpong" {
  name                = "sapingpong"
  resource_group_name = azurerm_resource_group.ping-pong.name
  location            = azurerm_resource_group.ping-pong.location
  account_tier        = "Standard"
  account_kind        = "StorageV2"
  account_replication_type  = "LRS"
  min_tls_version           = "TLS1_2"
  allow_blob_public_access  = true
  static_website {
    index_document = "index.html"
  }
}